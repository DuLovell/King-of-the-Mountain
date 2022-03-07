using Data;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Services.Environment;
using Services.Environment.Enemies;
using Services.Environment.Stairs;
using Services.Input;

namespace Infrastructure.States
{
	public class BootstrapState : IState
	{
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;
		private readonly ICoroutineRunner _coroutineRunner;


		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, 
			ICoroutineRunner coroutineRunner)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;
			_coroutineRunner = coroutineRunner;

			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(Config.INITIAL_SCENE_NAME, onLoaded: EnterLoadLevel);
		}

		public void Exit()
		{
		}

		private void RegisterServices()
		{
			_services.RegisterSingle<IInputService>(GetInputService());
			
			_services.RegisterSingle<IAssetProvider>(new AssetProvider());
			
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
			
			_services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
			
			_services.RegisterSingle<ISaveLoadService>(
				new SaveLoadService(_services.Single<IPersistentProgressService>(), 
					_services.Single<IGameFactory>()));
			
			_services.RegisterSingle<IStairsCountService>(new StairsCountService(_services.Single<IGameFactory>()));
			
			_services.RegisterSingle<IEnemySpawnService>(
				new EnemySpawnService(_services.Single<IStairsCountService>(),
					_services.Single<IGameFactory>(), _coroutineRunner));
			
			_services.RegisterSingle<IStairsPlacementService>(new StairsPlacementService(
				_services.Single<IGameFactory>(), 
				_services.Single<IStairsCountService>()));
		}

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadLevelState, string>(Config.GAMEPLAY_SCENE_NAME);

		private static IInputService GetInputService()
		{
			return new MobileInputService();
		}
	}
}
