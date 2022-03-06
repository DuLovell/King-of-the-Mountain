using Infrastructure.Factory;
using Logic.Movement;
using Services.Environment;
using Services.Environment.Enemies;
using Services.Environment.Stairs;
using UnityEngine;

namespace Infrastructure.States
{
	public class GameLoopState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly IStairsPlacementService _stairsPlacementService;
		private readonly IGameFactory _gameFactory;
		private readonly IEnemySpawnService _enemySpawnService;

		public GameLoopState(GameStateMachine stateMachine, IStairsPlacementService stairsPlacementService, 
			IGameFactory gameFactory, IEnemySpawnService enemySpawnService)
		{
			_stateMachine = stateMachine;
			_stairsPlacementService = stairsPlacementService;
			_gameFactory = gameFactory;
			_enemySpawnService = enemySpawnService;
		}

		public void Enter()
		{
			_gameFactory.Player.Mover.OnPlayerMoved +=
				newPlayerPosition =>
				{
					_stairsPlacementService.RearrangeStairs(newPlayerPosition);
				};
			_gameFactory.Player.OnPlayerDied += EnterGameOverState;
			
			_enemySpawnService.StartSpawningEnemies();
		}

		private void EnterGameOverState()
		{
			_stateMachine.Enter<GameOverState>();
		}

		public void Exit()
		{
			_enemySpawnService.StopSpawningEnemies();
			_gameFactory.Player.OnPlayerDied -= EnterGameOverState;
		}
	}
}
