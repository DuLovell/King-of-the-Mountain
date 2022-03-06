﻿using System;
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
		private IStairsMovement _stairsMovement;
		
		private void Awake()
		{
			_inputService = AllServices.Container.Single<IInputService>();
			_stairsMovement = GetComponent<IStairsMovement>();
		}

		private void OnEnable()
		{
			_inputService.OnTap += MoveVertically;
			_inputService.OnSwipe += MoveHorizontally;
			_stairsMovement.OnMoved += OnMoved;
		}

		private void OnDisable()
		{
			_inputService.OnTap -= MoveVertically;
			_inputService.OnSwipe -= MoveHorizontally;
			_stairsMovement.OnMoved -= OnMoved;
		}

		private void MoveVertically()
		{
			_stairsMovement.StartMoving(_stairsMovement.StairOffset);
		}

		private void MoveHorizontally(SwipeData swipeData)
		{
			switch (swipeData.Direction)
			{
				case SwipeDirection.Up:
				case SwipeDirection.Right:
					_stairsMovement.StartMoving(transform.right);
					break;
				case SwipeDirection.Down:
				case SwipeDirection.Left:
					_stairsMovement.StartMoving(-transform.right);
					break;
			}
		}

		private void OnMoved(Vector3 newPosition)
		{
			OnPlayerMoved?.Invoke(newPosition);
		}
	}
}