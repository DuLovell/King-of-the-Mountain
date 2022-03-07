using System;
using Infrastructure.Services;
using LootLocker.Requests;

namespace Services.Leaderboard
{
	public interface ILeaderboardService : IService
	{
		void SubmitScore(int score);
		void GetLeaderboard(int scoresCount, Action<LootLockerLeaderboardMember[]> onComplete);
	}
}
