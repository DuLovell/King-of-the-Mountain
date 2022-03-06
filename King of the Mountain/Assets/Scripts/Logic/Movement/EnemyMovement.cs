using System;
using System.Collections;
using UnityEngine;

namespace Logic.Movement
{
	[RequireComponent(typeof(IStairsMovement))]
	public class EnemyMovement : MonoBehaviour
	{
		private IStairsMovement _stairsMovement;

		private void Awake()
		{
			_stairsMovement = GetComponent<IStairsMovement>();
		}

		private void Start()
		{
			Coroutine _moveCoroutine = StartMoving();
		}

		private Coroutine StartMoving()
		{
			return StartCoroutine(MoveRoutine());

			IEnumerator MoveRoutine()
			{
				while (true)
				{
					_stairsMovement.StartMoving(-_stairsMovement.StairOffset);
					yield return new WaitForSeconds(1f);
				}
			}
		}
	}
}
