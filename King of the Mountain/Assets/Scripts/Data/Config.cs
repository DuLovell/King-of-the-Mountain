using UnityEngine;

namespace Data
{
	public static class Config
	{
		public const string GAMEPLAY_SCENE_NAME = "Gameplay";
		public const string INITIAL_SCENE_NAME = "Initial";
		
		public static readonly Vector3 PlayerStartPosition = Vector3.zero;
		public static readonly Vector3 StairOffset = new Vector3(0f, 1f, 1f);
		public const int STAIR_LENGTH = 6;
		public const int SIDE_MAX_STAIRS_COUNT = 10;
	}
}
