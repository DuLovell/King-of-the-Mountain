using System;
using UnityEngine;

namespace Logic.Damage
{
	[RequireComponent(typeof(Collider))]
	public class DestroyOnTriggerEnter : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IDestroyable destroyable))
			{
				destroyable.Destroy();
			}
		}
	}
}
