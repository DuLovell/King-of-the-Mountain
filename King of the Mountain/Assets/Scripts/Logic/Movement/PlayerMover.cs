using System;
using System.Collections;
using Data;
using DG.Tweening;
using Infrastructure.Services;
using Services.Input;
using UnityEngine;

namespace Logic.Movement
{
	[RequireComponent(typeof(IStairsMovement))]
	public class PlayerMover : MonoBehaviour
	{
		public event Action<Vector3> OnPlayerMoved;
		
		private IInputService _inputService;
		
		public IStairsMovement StairsMovement { get; private set; }

		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			StairsMovement = GetComponent<IStairsMovement>();
		}

		private void OnEnable()
		{
			_inputService.OnTap += MoveVertically;
			_inputService.OnSwipe += MoveHorizontally;
			StairsMovement.OnMoved += OnMoved;
		}

		private void OnDisable()
		{
			_inputService.OnTap -= MoveVertically;
			_inputService.OnSwipe -= MoveHorizontally;
			StairsMovement.OnMoved -= OnMoved;
		}

		private void MoveVertically()
		{
			StairsMovement.StartMoving(StairsMovement.StairOffset);
		}

		private void MoveHorizontally(SwipeData swipeData)
		{
			switch (swipeData.Direction)
			{
				case SwipeDirection.Down:
				case SwipeDirection.Right:
					StairsMovement.StartMoving(transform.right);
					break;
				case SwipeDirection.Up:
				case SwipeDirection.Left:
					StairsMovement.StartMoving(-transform.right);
					break;
			}
		}

		private void OnMoved(Vector3 newPosition)
		{
			OnPlayerMoved?.Invoke(newPosition);
		}
	}
}
