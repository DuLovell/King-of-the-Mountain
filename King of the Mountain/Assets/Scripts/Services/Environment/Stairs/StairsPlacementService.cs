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

		private readonly Vector3 _stairOffset = Config.StairOffset;

		private Queue<GameObject> _activeStairs;
		private Transform _stairsContainer;

		public StairsPlacementService(IGameFactory gameFactory, IStairsCountService stairsCountService)
		{
			_gameFactory = gameFactory;
			_stairsCountService = stairsCountService;
		}

		public void RearrangeStairs(Vector3 centerStairPosition)
		{
			int stairsDelta = _stairsCountService.CalculateStairsDelta(centerStairPosition);
			Debug.Log($"size delta={stairsDelta}");
			for (int i = 0; i < stairsDelta; i++)
			{
				RemoveStair();
				PlaceStair(_activeStairs.Last().transform.position + _stairOffset);
			}
		}

		public void PlaceStairs(Vector3 centerStairPosition)
		{
			_activeStairs = new Queue<GameObject>();
			_stairsContainer = new GameObject(STAIRS_CONTAINER_NAME).transform;
			
			for (int i = -SIDE_MAX_STAIRS_COUNT; i <= SIDE_MAX_STAIRS_COUNT; i++)
			{
				PlaceStair(centerStairPosition + _stairOffset * i);
			}
		}

		private void PlaceStair(Vector3 position)
		{
			GameObject stair = _gameFactory.CreateStair(position);
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
