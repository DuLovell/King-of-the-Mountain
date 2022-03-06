using Infrastructure.Factory;
using Logic.Movement;
using Services.Environment;
using Services.Environment.Stairs;
using UnityEngine;

namespace Infrastructure.States
{
	public class GameLoopState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IStairsPlacementService _stairsPlacementService;
		private readonly IGameFactory _gameFactory;

		public GameLoopState(GameStateMachine stateMachine, IStairsPlacementService stairsPlacementService, IGameFactory gameFactory)
		{
			_stateMachine = stateMachine;
			_stairsPlacementService = stairsPlacementService;
			_gameFactory = gameFactory;
		}

		public void Enter()
		{
			_gameFactory.Player.GetComponent<PlayerMover>().OnPlayerMoved +=
				newPlayerPosition =>
				{
					_stairsPlacementService.RearrangeStairs(newPlayerPosition);
				};
		}

		public void Exit()
		{
		}
	}
}
