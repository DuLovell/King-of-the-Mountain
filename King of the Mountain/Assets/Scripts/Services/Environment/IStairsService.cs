using Infrastructure.Services;
using UnityEngine;

namespace Services.Environment
{
	public interface IStairsService : IService
	{
		void PlaceStairs(Transform transformPoint);
	}
}
