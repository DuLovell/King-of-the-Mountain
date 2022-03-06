using System;
using Data;
using DG.Tweening;
using UnityEngine;

namespace Logic.Movement
{
	public class StairsMovement : MonoBehaviour, IStairsMovement
	{
		[SerializeField] private float _jumpPower = 0.5f;
		[SerializeField] private int _jumps = 1;
		[SerializeField] private float _jumpDuration = 1f;

		private Vector3 _newPosition;
		
		public Vector3 StairOffset => Config.StairOffset;

		public void StartMoving(Vector3 deltaPosition)
		{
			_newPosition += deltaPosition;
			transform.DOJump(_newPosition, _jumpPower, _jumps, _jumpDuration);
		}

		private void Start()
		{
			_newPosition = transform.position;
		}
	}
}
