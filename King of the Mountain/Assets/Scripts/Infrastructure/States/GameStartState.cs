using Infrastructure.Factory;
using Logic.View.Screens;
using Services.Environment.Stairs;

namespace Infrastructure.States
{
	public class GameStartState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IGameFactory _gameFactory;
		private readonly IStairsCountService _stairsCountService;

		private StartScreen _hudStartScreen;

		public GameStartState(GameStateMachine stateMachine, IGameFactory gameFactory, IStairsCountService stairsCountService)
		{
			_stateMachine = stateMachine;
			_gameFactory = gameFactory;
			_stairsCountService = stairsCountService;
		}

		public void Enter()
		{
			_hudStartScreen = _gameFactory.Hud.ShowStartScreen();
			_hudStartScreen.OnGameStarted += _stateMachine.Enter<GameLoopState>;
			_stairsCountService.ResetCount();
		}

		public void Exit()
		{
			_hudStartScreen.OnGameStarted -= _stateMachine.Enter<GameLoopState>;
		}
	}
}
