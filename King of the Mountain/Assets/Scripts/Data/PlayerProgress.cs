using System;
using Infrastructure.AssetManagement;
using Infrastructure.Services;

namespace Data
{
	[Serializable]
	public class PlayerProgress
	{
		public string Name;
		public int BestScore;
		
		public PlayerProgress(string playerName)
		{
			Name = playerName;
		}
	}
}
