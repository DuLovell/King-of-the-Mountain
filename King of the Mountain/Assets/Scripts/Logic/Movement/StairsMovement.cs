using System;
using System.Collections;
using Data;
using DG.Tweening;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Logic.Movement
{
	public class StairsMovement : MonoBehaviour
	{
		[SerializeField] private float _jumpPower = 0.5f;
		[SerializeField] private int _jumps = 1;
		[SerializeField] private float _jumpDuration = 1f;

		private readonly Vector3 _stairOffset = Config.StairOffset;
		
		private IInputService _inputService;

		private bool _isMoving;

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
		}

		private void OnEnable()
		{
			_inputService.OnTap += MoveVertically;
			_inputService.OnSwipe += MoveHorizontally;
		}

		private void OnDisable()
		{
			_inputService.OnTap -= MoveVertically;
			_inputService.OnSwipe -= MoveHorizontally;
		}

		private void MoveVertically()
		{
			StartMoving(_stairOffset);
		}

		private void MoveHorizontally(SwipeData swipeData)
		{
			switch (swipeData.Direction)
			{
				case SwipeDirection.Up:
				case SwipeDirection.Right:
					StartMoving(transform.right);
					break;
				case SwipeDirection.Down:
				case SwipeDirection.Left:
					StartMoving(-transform.right);
					break;
			}
		}

		private void StartMoving(Vector3 deltaPosition)
		{
			if (_isMoving) return;
			
			Vector3 newPosition = transform.position + deltaPosition;
			transform.DOJump(newPosition, _jumpPower, _jumps, _jumpDuration)
				.OnComplete(() => _isMoving = false);

			_isMoving = true;
		}
	}
}
