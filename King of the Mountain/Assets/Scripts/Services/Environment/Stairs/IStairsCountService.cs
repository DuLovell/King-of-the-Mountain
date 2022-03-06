using System;
using Infrastructure.Services;
using UnityEngine;

namespace Services.Environment.Stairs
{
	public interface IStairsCountService : IService
	{
		event Action<int> OnPlayerStairPositionChanged;
		int LastPlayerPositionStairNumber { get; }
		int CalculateStairsDelta(Vector3 newPosition);
		void ResetCount();
	}
}
