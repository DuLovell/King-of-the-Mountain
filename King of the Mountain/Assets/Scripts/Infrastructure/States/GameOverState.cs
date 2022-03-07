using System.Collections;
using Data;
using Infrastructure.Factory;
using Infrastructure.Services.SaveLoad;
using Logic.View.Screens;

namespace Infrastructure.States
{
	public class GameOverState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IGameFactory _gameFactory;
		private readonly SceneLoader _sceneLoader;

		private GameOverScreen _gameOverScreen;
		private readonly ISaveLoadService _saveloadProgress;

		public GameOverState(GameStateMachine stateMachine, IGameFactory gameFactory, SceneLoader sceneLoader, ISaveLoadService saveloadProgress)
		{
			_stateMachine = stateMachine;
			_gameFactory = gameFactory;
			_sceneLoader = sceneLoader;
			_saveloadProgress = saveloadProgress;
		}

		public void Enter()
		{
			_saveloadProgress.SaveProgress();
			_saveloadProgress.InformProgressReaders();
			_gameOverScreen = _gameFactory.Hud.ShowGameOverScreen();
			_gameOverScreen.OnGameOverPressed += RestartGame;
		}

		private void RestartGame()
		{
			_sceneLoader.Load("Initial", onLoaded: EnterLoadLevelState);
		}

		private void EnterLoadLevelState()
		{
			_stateMachine.Enter<LoadLevelState, string>(Config.GAMEPLAY_SCENE_NAME);
		}

		public void Exit()
		{
			_gameOverScreen.OnGameOverPressed -= RestartGame;
		}
	}
}
