using Infrastructure.Services;
using UnityEngine;

namespace Services.Environment.Stairs
{
	public interface IStairsPlacementService : IService
	{
		void PlaceStairs(Vector3 centerStairPosition);
		void RearrangeStairs(Vector3 centerStairPosition);
	}
}
