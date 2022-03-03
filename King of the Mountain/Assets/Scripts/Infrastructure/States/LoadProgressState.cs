using Data;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.States
{
	public class LoadProgressState : IState
	{
		private const string GAMEPLAY_SCENE_NAME = "Gameplay";

		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly ISaveLoadService _saveLoadProgress;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService,
			ISaveLoadService saveLoadProgress)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_saveLoadProgress = saveLoadProgress;
		}

		public void Enter()
		{
			LoadProgressOrInitNew();

			_gameStateMachine.Enter<LoadLevelState, string>(GAMEPLAY_SCENE_NAME);
		}

		public void Exit()
		{
		}

		private void LoadProgressOrInitNew()
		{
			_progressService.Progress =
				_saveLoadProgress.LoadProgress()
				?? NewProgress();
		}

		private PlayerProgress NewProgress() =>
			new PlayerProgress();
	}
}
