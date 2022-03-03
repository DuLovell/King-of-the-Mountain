using Infrastructure.Factory;
using UnityEngine;

namespace Services.Environment
{
	public class StairsService : IStairsService
	{
		private const int SIDE_MAX_STAIRS_COUNT = 10;
		
		private readonly IGameFactory _gameFactory;
		
		private readonly Vector3 _stairOffset = new Vector3(0f, 1f, 1f);

		public StairsService(IGameFactory gameFactory)
		{
			_gameFactory = gameFactory;
		}

		public void PlaceStairs(Transform transformPoint)
		{
			for (int i = -SIDE_MAX_STAIRS_COUNT; i <= SIDE_MAX_STAIRS_COUNT; i++)
			{
				_gameFactory.CreateStair(transformPoint.position + _stairOffset * i, transformPoint.forward);
			}
		}
	}
}
