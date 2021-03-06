using Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Logic;
using Services.Environment;
using Services.Environment.Stairs;
using UnityEngine;

namespace Infrastructure.States
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IPersistentProgressService _progressService;
		private readonly IStairsPlacementService _stairsPlacementService;
		
		private readonly Vector3 _playerStartPosition = Config.PlayerStartPosition;

		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			IGameFactory gameFactory, IPersistentProgressService progressService, IStairsPlacementService stairsPlacementService)
		{
			_stateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_progressService = progressService;
			_stairsPlacementService = stairsPlacementService;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
			
			_stateMachine.Enter<LoadProgressState>();
		}

		private void InitGameWorld()
		{
			Player player = _gameFactory.CreatePlayer(_playerStartPosition);
			_gameFactory.CreateFollowCamera(player.transform);
			_stairsPlacementService.PlaceStairs(player.transform.position);
			_gameFactory.CreateHud();
		}
	}
}
