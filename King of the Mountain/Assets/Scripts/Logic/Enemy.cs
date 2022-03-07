using System;
using Logic.Movement;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(RendererVisibilityReporter))]
	[RequireComponent(typeof(IStairsMovement))]
	public class Enemy : MonoBehaviour
	{
		public RendererVisibilityReporter VisibilityReporter { get; private set; }
		public IStairsMovement Mover { get; private set; }

		private void Awake()
		{
			VisibilityReporter = GetComponent<RendererVisibilityReporter>();
			Mover = GetComponent<IStairsMovement>();
		}
	}
}
