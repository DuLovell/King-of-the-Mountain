using System;
using Infrastructure.Factory;
using Infrastructure.Services;
using LootLocker.Requests;
using Services.Leaderboard;
using UnityEngine;

namespace Logic.View.Leaderboard
{
	public class LeaderboardView : MonoBehaviour
	{
		[SerializeField] private Transform _scoresContainer;
		
		private const int LEADERBOARD_SIZE = 5;

		private IGameFactory _gameFactory;
		private ILeaderboardService _leaderboardService;

		private void Awake()
		{
			_gameFactory = AllServices.Container.Single<IGameFactory>();
			_leaderboardService = AllServices.Container.Single<ILeaderboardService>();
		}

		private void OnEnable()
		{
			_leaderboardService.GetLeaderboard(LEADERBOARD_SIZE, onComplete: UpdateLeaderboard);
		}

		private void UpdateLeaderboard(LootLockerLeaderboardMember[] scores)
		{
			foreach (LootLockerLeaderboardMember score in scores)
			{
				print($"player_id={score.member_id}, score={score.score}");
				_gameFactory.CreateScoreItem(score.rank, score.score, _scoresContainer);
			}
		}
	}
}
