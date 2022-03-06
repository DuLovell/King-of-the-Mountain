using Infrastructure.Factory;

namespace Infrastructure.States
{
	public class GameOverState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IGameFactory _gameFactory;

		public GameOverState(GameStateMachine stateMachine, IGameFactory gameFactory)
		{
			_stateMachine = stateMachine;
			_gameFactory = gameFactory;
		}

		public void Enter()
		{
			_gameFactory.FollowCamera.SetActive(false);
		}

		public void Exit()
		{
			
		}
	}
}
