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
		
		//TODO Перенести в общий конфиг
		private readonly Vector3 _playerStartPosition = Vector3.zero;

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
			_gameFactory.Cleanup();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();
			InformProgressReaders();

			_stateMachine.Enter<GameLoopState>();
		}

		private void InformProgressReaders()
		{
			foreach (ISavedProgressReader progressReader in _gameFactory.ProgressReaders)
				progressReader.LoadProgress(_progressService.Progress);
		}

		private void InitGameWorld()
		{
			GameObject player = _gameFactory.CreatePlayer(_playerStartPosition);
			_stairsPlacementService.PlaceStairs(player.transform);
		}
	}
}
