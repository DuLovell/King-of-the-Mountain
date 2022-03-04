using Infrastructure.Services;
using UnityEngine;

namespace Services.Environment.Stairs
{
	public interface IStairsPlacementService : IService
	{
		void PlaceStairs(Transform transformPoint);
		void RearrangeStairs(Transform playerTransform);
	}
}
