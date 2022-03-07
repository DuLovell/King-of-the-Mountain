using System;
using Data;
using DG.Tweening;
using UnityEngine;

namespace Logic.Movement
{
	public class StairsMovement : MonoBehaviour, IStairsMovement
	{
		public event Action<Vector3> OnMoved;
		
		[SerializeField] private float _jumpPower = 0.5f;
		[SerializeField] private int _jumps = 1;
		[SerializeField] private float _jumpDuration = 1f;

		private Vector3 _newPosition;
		
		public Vector3 StairOffset => Config.StairOffset;

		public void SetStartPosition(Vector3 position)
		{
			transform.position = position;
			_newPosition = position;
		}

		public void StartMoving(Vector3 deltaPosition)
		{
			_newPosition += deltaPosition;
			transform.DOJump(_newPosition, _jumpPower, _jumps, _jumpDuration);

			OnMoved?.Invoke(_newPosition);
		}
	}
}
