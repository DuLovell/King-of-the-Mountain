using Infrastructure.Services;
using UnityEngine;

namespace Services.Environment.Stairs
{
	public interface IStairsCountService : IService
	{
		int CalculateStairsDelta(Vector3 newPosition);
	}
}
