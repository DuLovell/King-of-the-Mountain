using System;
using Logic.Damage;
using Logic.Movement;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(PlayerMover))]
	public class Player : MonoBehaviour, IDestroyable
	{
		public event Action OnPlayerDied;

		private Rigidbody _rigidbody;
		private RendererVisibilityReporter _rendererVisibilityReporter;

		public PlayerMover Mover { get; private set; }

		private void Awake()
		{
			Mover = GetComponent<PlayerMover>();
			_rigidbody = GetComponent<Rigidbody>();
			_rendererVisibilityReporter = GetComponent<RendererVisibilityReporter>();
		}

		private void OnEnable()
		{
			Mover.OnPlayerMoved += CheckGround;
			_rendererVisibilityReporter.OnBecomeInvisible += Destroy;
		}

		private void OnDisable()
		{
			Mover.OnPlayerMoved -= CheckGround;
			_rendererVisibilityReporter.OnBecomeInvisible -= Destroy;
		}

		private void CheckGround(Vector3 position)
		{
			if (Physics.Raycast(position + Vector3.up, -transform.up, Mathf.Infinity)) return;
			
			OnPlayerDied?.Invoke();
				
			_rigidbody.isKinematic = false;
		}

		public void Destroy()
		{
			OnPlayerDied?.Invoke();
			Destroy(gameObject);
		}
	}
}
