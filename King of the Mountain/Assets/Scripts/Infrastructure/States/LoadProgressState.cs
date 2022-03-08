using Data;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using Logic.View;
using Logic.View.Screens;

namespace Infrastructure.States
{
	public class LoadProgressState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadProgress;
		private readonly IGameFactory _gameFactory;
		
		private HelloScreen _helloScreen;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
			ISaveLoadService saveLoadProgress, IGameFactory gameFactory)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadProgress = saveLoadProgress;
			_gameFactory = gameFactory;
		}

		public void Enter()
		{
			if (!TryLoadProgress())
			{
				_helloScreen = _gameFactory.Hud.ShowHelloScreen();
				_helloScreen.OnNameEntered += LoadLevelWithNewProgress;
			}
			else
			{
				EnterGameStartState();
			}
		}

		private void LoadLevelWithNewProgress(string playerName)
		{
			CreateNewProgress(playerName);
			EnterGameStartState();
		}

		private void EnterGameStartState()
		{
			_gameStateMachine.Enter<GameStartState>();
		}

		private bool TryLoadProgress()
		{
			_progressService.Progress = _saveLoadProgress.LoadProgress();

			return _progressService.Progress != null;
		}

		private void CreateNewProgress(string playerName)
		{
			_progressService.Progress = new PlayerProgress(playerName);
		}

		public void Exit()
		{
			if (_helloScreen != null)
			{
				_helloScreen.OnNameEntered -= CreateNewProgress;
			}
			
			_saveLoadProgress.SaveProgress();
			_saveLoadProgress.InformProgressReaders();
		}
	}
}
