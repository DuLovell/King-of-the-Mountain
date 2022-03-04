using Infrastructure.Factory;
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
		}

		public void Exit()
		{
		}
	}
}
