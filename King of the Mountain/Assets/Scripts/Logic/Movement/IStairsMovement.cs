using UnityEngine;

namespace Logic.Movement
{
	public interface IStairsMovement
	{
		Vector3 StairOffset { get; }
		void StartMoving(Vector3 deltaPosition);
	}
}
