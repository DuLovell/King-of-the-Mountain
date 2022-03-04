using Data;
using UnityEngine;

namespace Services.Environment.Stairs
{
	public class StairsCountService : IStairsCountService
	{
		private readonly Vector3 _stairOffset = Config.StairOffset;

		private int _lastPlayerPositionStairNumber;
		
		private Vector3 _lastPlayerPosition = Config.PlayerStartPosition;
		
		public int CalculateStairsDelta(Vector3 newPosition)
		{
			int stairsDelta = (int) (newPosition.y - _lastPlayerPosition.y) / (int) _stairOffset.y;
			int stairNumber = _lastPlayerPositionStairNumber + stairsDelta;

			_lastPlayerPositionStairNumber = stairNumber;
			_lastPlayerPosition = newPosition;

			return stairsDelta;
		}
	}
}
