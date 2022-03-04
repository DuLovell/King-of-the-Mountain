using UnityEngine;

namespace Services.Environment.Stairs
{
	public class StairsCountService : IStairsCountService
	{
		private readonly Vector3 _stairOffset = new Vector3(0f, 1f, 1f);

		private int _lastPlayerPositionStairNumber;
		
		//TODO Использовать playerStartPosition
		private Vector3 _lastPlayerPosition = Vector3.zero;
		
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
