using System;
using Infrastructure.Services;
using UnityEngine;

namespace Services.Input
{
	public interface IInputService : IService
	{
		event Action<SwipeData> OnSwipe;
		void InvokeOnSwipe(SwipeData swipeData);
	}
}
