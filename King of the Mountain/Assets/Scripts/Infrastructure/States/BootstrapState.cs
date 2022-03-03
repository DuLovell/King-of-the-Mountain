﻿using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Services.Input;

namespace Infrastructure.States
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";
		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;

			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
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
				new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
		}

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadProgressState>();

		private static IInputService GetInputService()
		{
			return new MobileInputService();
		}
	}
}