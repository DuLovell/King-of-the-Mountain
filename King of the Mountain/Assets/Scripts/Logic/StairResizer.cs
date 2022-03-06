using System;
using Data;
using UnityEngine;

namespace Logic
{
	public class StairResizer : MonoBehaviour
	{
		[SerializeField] private GameObject _objectToResize;

		private void Awake()
		{
			Vector3 stairScale = _objectToResize.transform.localScale;
			stairScale.x = Config.StairLength;
			_objectToResize.transform.localScale = stairScale;
		}
	}
}
