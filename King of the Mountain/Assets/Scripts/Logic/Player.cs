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
		public event Action OnPlayerFell;

		private Rigidbody _rigidbody;
		private RendererVisibilityReporter _visibilityReporter;

		public PlayerMover Mover { get; private set; }

		private void Awake()
		{
			Mover = GetComponent<PlayerMover>();
			_rigidbody = GetComponent<Rigidbody>();
			_visibilityReporter = GetComponent<RendererVisibilityReporter>();
		}

		private void OnEnable()
		{
			Mover.OnPlayerMoved += CheckGround;
			_visibilityReporter.OnBecomeInvisible += Destroy;
		}

		private void OnDisable()
		{
			Mover.OnPlayerMoved -= CheckGround;
			_visibilityReporter.OnBecomeInvisible -= Destroy;
		}

		private void CheckGround(Vector3 position)
		{
			if (Physics.Raycast(position + Vector3.up, -transform.up, Mathf.Infinity)) return;
			
			OnPlayerFell?.Invoke();

			Mover.enabled = false;
			_rigidbody.isKinematic = false;
		}

		public void Destroy()
		{
			OnPlayerDied?.Invoke();
			Destroy(gameObject);
		}
	}
}
