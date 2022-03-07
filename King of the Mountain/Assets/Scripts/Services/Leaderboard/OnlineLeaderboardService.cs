using System;
using Infrastructure.Services.PersistentProgress;
using LootLocker.Requests;
using UnityEngine;

namespace Services.Leaderboard
{
	public class OnlineLeaderboardService : ILeaderboardService
	{
		private const int LEADERBOARD_ID = 1835;

		private readonly IPersistentProgressService _progressService;

		public OnlineLeaderboardService(IPersistentProgressService progressService)
		{
			_progressService = progressService;
		}

		public void SubmitScore(int score)
		{
			LootLockerSDKManager.StartGuestSession(SystemInfo.deviceUniqueIdentifier,
				response =>
				{
#if DEBUG
					Debug.Log(response.success ? "Session successfully started" : "Error when starting session");
#endif
				});
			
			string memberId = _progressService.Progress.Name;
			LootLockerSDKManager.SubmitScore(memberId, score, LEADERBOARD_ID,
				response =>
				{
#if DEBUG
					Debug.Log(response.success ? "Score successfully submitted" : "Error when submitting score");
#endif
				});
		}

		public void GetLeaderboard(int scoresCount, Action<LootLockerLeaderboardMember[]> onComplete)
		{
			LootLockerSDKManager.GetScoreList(LEADERBOARD_ID, scoresCount, response => onComplete.Invoke(response.items));
		}
	}
}
