using System;
using UnityEngine;

namespace Services.Input
{
	public class MobileInputService : IInputService
	{
		public event Action<SwipeData> OnSwipe;

		public void InvokeOnSwipe(SwipeData swipeData)
		{
			OnSwipe?.Invoke(swipeData);
		}
	}
}
