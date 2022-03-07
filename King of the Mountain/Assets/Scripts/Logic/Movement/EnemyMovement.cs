using System;
using System.Collections;
using UnityEngine;

namespace Logic.Movement
{
	[RequireComponent(typeof(IStairsMovement))]
	public class EnemyMovement : MonoBehaviour
	{
		private readonly WaitForSeconds _nextMoveDelay = new WaitForSeconds(1f);
		
		private Coroutine _moveCoroutine;
		
		public IStairsMovement StairsMovement { get; private set; }

		private void Awake()
		{
			StairsMovement = GetComponent<IStairsMovement>();
		}

		private void OnDisable()
		{
			StopMoving();
		}

		public void StartMoving()
		{
			_moveCoroutine = StartCoroutine(MoveRoutine());

			IEnumerator MoveRoutine()
			{
				while (true)
				{
					StairsMovement.StartMoving(-StairsMovement.StairOffset);
					yield return _nextMoveDelay;
				}
			}
		}

		public void StopMoving()
		{
			if (_moveCoroutine == null) return;
			
			StopCoroutine(_moveCoroutine);
			_moveCoroutine = null;
		}
	}
}
