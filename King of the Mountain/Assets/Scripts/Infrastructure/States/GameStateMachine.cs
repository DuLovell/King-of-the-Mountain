using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Logic;
using Services.Environment;
using Services.Environment.Enemies;
using Services.Environment.Stairs;

namespace Infrastructure.States
{
	public class GameStateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services,
			ICoroutineRunner coroutineRunner)
		{
			_states = new Dictionary<Type, IExitableState>
			{
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, coroutineRunner),
				
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain,
					services.Single<IGameFactory>(), 
					services.Single<IPersistentProgressService>(), 
					services.Single<IStairsPlacementService>()),
				
				[typeof(LoadProgressState)] = new LoadProgressState(this, 
					services.Single<IPersistentProgressService>(),
					services.Single<ISaveLoadService>()),
				
				[typeof(GameLoopState)] = new GameLoopState(this,  
					services.Single<IStairsPlacementService>(),
					services.Single<IGameFactory>(),
					services.Single<IEnemySpawnService>()),
				
				[typeof(GameOverState)] = new GameOverState(this,
					services.Single<IGameFactory>()),
			};
		}

		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
		{
			TState state = ChangeState<TState>();
			state.Enter(payload);
		}

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();

			TState state = GetState<TState>();
			_activeState = state;

			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;
	}
}
