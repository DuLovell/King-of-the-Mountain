using System;
using Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Services.Environment.Stairs
{
	public class StairsCountService : IStairsCountService, ISavedProgress
	{
		public event Action<int> OnPlayerStairPositionChanged;
		
		private readonly Vector3 _stairOffset = Config.StairOffset;
		private Vector3 _lastPlayerPosition;

		public int LastPlayerPositionStairNumber { get; private set; }
		public int BestPlayerPositionStairNumber { get; private set; }

		public StairsCountService(IGameFactory gameFactory)
		{
			gameFactory.Register(this);
		}

		public int CalculateStairsDelta(Vector3 newPosition)
		{
			int stairsDelta = (int) (newPosition.y - _lastPlayerPosition.y) / (int) _stairOffset.y;
			int stairNumber = LastPlayerPositionStairNumber + stairsDelta;

			LastPlayerPositionStairNumber = stairNumber;
			_lastPlayerPosition = newPosition;

			if (stairsDelta != 0)
			{
				OnPlayerStairPositionChanged?.Invoke(stairNumber);
			}

			return stairsDelta;
		}

		public void ResetCount()
		{
			LastPlayerPositionStairNumber = 0;
			_lastPlayerPosition = Config.PlayerStartPosition;
		}

		public void LoadProgress(PlayerProgress progress)
		{
			BestPlayerPositionStairNumber = progress.BestScore;
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			if (LastPlayerPositionStairNumber > progress.BestScore)
			{
				
				progress.BestScore = LastPlayerPositionStairNumber;
			}
		}
	}
}
