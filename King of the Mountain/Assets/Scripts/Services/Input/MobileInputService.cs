using System;
using Logic.Input;
using UnityEngine;

namespace Services.Input
{
	public class MobileInputService : IInputService
	{
		public event Action<SwipeData> OnSwipe;
		public event Action OnTap;

		public void InvokeOnSwipe(SwipeData swipeData)
		{
			OnSwipe?.Invoke(swipeData);
		}

		public void InvokeOnTap()
		{
			OnTap?.Invoke();
		}
	}
}
