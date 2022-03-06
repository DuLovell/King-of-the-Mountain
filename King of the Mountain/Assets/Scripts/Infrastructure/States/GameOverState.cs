using System.Collections;
using Infrastructure.Factory;
using Logic.View.Screens;

namespace Infrastructure.States
{
	public class GameOverState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IGameFactory _gameFactory;
		private readonly SceneLoader _sceneLoader;

		private GameOverScreen _gameOverScreen;

		public GameOverState(GameStateMachine stateMachine, IGameFactory gameFactory, SceneLoader sceneLoader)
		{
			_stateMachine = stateMachine;
			_gameFactory = gameFactory;
			_sceneLoader = sceneLoader;
		}

		public void Enter()
		{
			_gameOverScreen = _gameFactory.Hud.ShowGameOverScreen();
			_gameOverScreen.OnGameOverPressed += RestartGame;
		}

		private void RestartGame()
		{
			_sceneLoader.Load("Initial", onLoaded: _stateMachine.Enter<LoadProgressState>);
		}

		public void Exit()
		{
			_gameOverScreen.OnGameOverPressed -= RestartGame;
		}
	}
}
