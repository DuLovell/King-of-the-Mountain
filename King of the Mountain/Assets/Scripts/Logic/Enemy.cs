using System;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(RendererVisibilityReporter))]
	public class Enemy : MonoBehaviour
	{
		public RendererVisibilityReporter VisibilityReporter { get; private set; }

		private void Awake()
		{
			VisibilityReporter = GetComponent<RendererVisibilityReporter>();
		}
	}
}
