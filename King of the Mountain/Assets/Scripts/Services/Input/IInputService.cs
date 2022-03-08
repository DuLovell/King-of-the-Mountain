using System;
using Infrastructure.Services;
using Logic.Input;
using UnityEngine;

namespace Services.Input
{
	public interface IInputService : IService
	{
		event Action<SwipeData> OnSwipe;
		event Action OnTap;
		void InvokeOnSwipe(SwipeData swipeData);
		void InvokeOnTap();
	}
}
