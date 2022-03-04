using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Factory;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.Environment.Stairs
{
	//TODO Реализовать object pool
	public class StairsPlacementService : IStairsPlacementService
	{
		private const int SIDE_MAX_STAIRS_COUNT = 10;
		private const string STAIRS_CONTAINER_NAME = "Stairs";

		private readonly IGameFactory _gameFactory;
		private readonly IStairsCountService _stairsCountService;
		
		private readonly Queue<GameObject> _activeStairs = new Queue<GameObject>();
		private readonly Vector3 _stairOffset = Config.StairOffset;

		private Transform _stairsContainer;

		public StairsPlacementService(IGameFactory gameFactory, IStairsCountService stairsCountService)
		{
			_gameFactory = gameFactory;
			_stairsCountService = stairsCountService;
		}

		public void RearrangeStairs(Transform transformPoint)
		{
			int stairsDelta = _stairsCountService.CalculateStairsDelta(transformPoint.position);

			for (int i = 0; i < stairsDelta; i++)
			{
				RemoveStair();
				PlaceStair(_activeStairs.Last().transform.position + _stairOffset, transformPoint.forward);
			}
		}

		public void PlaceStairs(Transform transformPoint)
		{
			_stairsContainer = new GameObject(STAIRS_CONTAINER_NAME).transform;
			
			for (int i = -SIDE_MAX_STAIRS_COUNT; i <= SIDE_MAX_STAIRS_COUNT; i++)
			{
				PlaceStair(transformPoint.position + _stairOffset * i, transformPoint.forward);
			}
		}

		private void PlaceStair(Vector3 position, Vector3 lookDirection)
		{
			GameObject stair = _gameFactory.CreateStair(position, lookDirection);
			stair.transform.parent = _stairsContainer;
			
			_activeStairs.Enqueue(stair);
		}

		private void RemoveStair()
		{
			GameObject removedStair = _activeStairs.Dequeue();
			Object.Destroy(removedStair);
		}
	}
}
