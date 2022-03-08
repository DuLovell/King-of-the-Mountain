using System.Collections.Generic;
using System.Linq;
using Data;
using Infrastructure.Factory;
using Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Services.Environment.Stairs
{
	public class StairsPlacementService : IStairsPlacementService
	{
		private const string STAIRS_CONTAINER_NAME = "Stairs";
		private const int STAIRS_DEFAULT_POOL_CAPACITY = Config.SIDE_MAX_STAIRS_COUNT * 2 + 1;

		private readonly IStairsCountService _stairsCountService;

		private readonly Vector3 _stairOffset = Config.StairOffset;

		private Queue<Stair> _activeStairs;
		private Transform _stairsContainer;

		private readonly MonoPool<Stair> _stairsPool;

		public StairsPlacementService(IGameFactory gameFactory, IStairsCountService stairsCountService)
		{
			_stairsCountService = stairsCountService;
			
			_stairsPool = new MonoPool<Stair>(
				createFunction: () => gameFactory.CreateStair(Vector3.zero),
				actionOnGet: (stair) => stair.gameObject.SetActive(true),
				actionOnRelease: (stair) => stair.gameObject.SetActive(false),
				actionOnDestroy: Object.Destroy, 
				defaultCapacity: STAIRS_DEFAULT_POOL_CAPACITY,
				autoExpand: true);
		}

		public void RearrangeStairs(Vector3 centerStairPosition)
		{
			int stairsDelta = _stairsCountService.CalculateStairsDelta(centerStairPosition);
			
			for (int i = 0; i < stairsDelta; i++)
			{
				RemoveStair();
				PlaceStair(_activeStairs.Last().transform.position + _stairOffset);
			}
		}

		public void PlaceStairs(Vector3 centerStairPosition)
		{
			_stairsContainer = new GameObject(STAIRS_CONTAINER_NAME).transform;
			_stairsPool.CreatePool(_stairsContainer);
			_activeStairs = new Queue<Stair>();

			for (int i = -Config.SIDE_MAX_STAIRS_COUNT; i <= Config.SIDE_MAX_STAIRS_COUNT; i++)
			{
				PlaceStair(centerStairPosition + _stairOffset * i);
			}
		}

		private void PlaceStair(Vector3 position)
		{
			Stair stair = _stairsPool.GetElement();
			stair.transform.position = position;
			
			_activeStairs.Enqueue(stair);
		}

		private void RemoveStair()
		{
			Stair removedStair = _activeStairs.Dequeue();
			_stairsPool.ReleaseElement(removedStair);
		}
	}
}
