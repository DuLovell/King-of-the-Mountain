using System;
using Logic.Movement;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(RendererVisibilityReporter))]
	[RequireComponent(typeof(EnemyMovement))]
	public class Enemy : MonoBehaviour
	{
		public event Action<Enemy> OnBecameInvisible;
		
		private RendererVisibilityReporter _visibilityReporter;
		private EnemyMovement _mover;

		public void StartMoving(Vector3 startPosition)
		{
			_mover.StairsMovement.SetStartPosition(startPosition);
			_mover.StartMoving();
		}

		public void StopMoving()
		{
			_mover.StopMoving();
		}
		
		private void Awake()
		{
			_visibilityReporter = GetComponent<RendererVisibilityReporter>();
			_mover = GetComponent<EnemyMovement>();
		}

		private void OnEnable()
		{
			_visibilityReporter.OnBecomeInvisible += InvokeOnBecameInvisible;
		}

		private void OnDisable()
		{
			_visibilityReporter.OnBecomeInvisible -= InvokeOnBecameInvisible;
		}

		private void InvokeOnBecameInvisible()
		{
			OnBecameInvisible?.Invoke(this);
		}
	}
}
