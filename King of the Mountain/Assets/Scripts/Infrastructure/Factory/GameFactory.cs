
using System.Collections.Generic;
using Cinemachine;
using Infrastructure.AssetManagement;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;

		public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();

		public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
		
		public GameObject Player { get; private set; }

		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
		}

		public void CreateHud() =>
			InstantiateRegistered(AssetPath.HudPath);

		public void RegisterProgressWatchers(GameObject gameObject)
		{
			foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
			{
				Register(progressReader);
			}
		}

		public void Register(ISavedProgressReader progressReader)
		{
			if (progressReader is ISavedProgress progressWriter)
				ProgressWriters.Add(progressWriter);

			ProgressReaders.Add(progressReader);
		}

		public GameObject CreatePlayer(Vector3 position)
		{
			GameObject player = _assets.Instantiate(AssetPath.PlayerPath, position);
			Player = player;
			
			return player;
		}

		public GameObject CreateFollowCamera(Transform target)
		{
			CinemachineVirtualCamera followCamera = _assets.Instantiate<CinemachineVirtualCamera>(AssetPath.FollowCameraPath);
			followCamera.Follow = target;
			followCamera.LookAt = target;
			
			return followCamera.gameObject;
		}

		public GameObject CreateStair(Vector3 position, Vector3 lookDirection)
		{
			return _assets.Instantiate(AssetPath.StairPath, position, Quaternion.LookRotation(lookDirection));
		}

		public GameObject CreateSwipeDetector()
		{
			return _assets.Instantiate<SwipeDetector>(AssetPath.SwipeDetectorPath).gameObject;
		}

		public void Cleanup()
		{
			ProgressReaders.Clear();
			ProgressWriters.Clear();
		}

		private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
		{
			GameObject gameObject = _assets.Instantiate(path: prefabPath, position);

			RegisterProgressWatchers(gameObject);
			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate<GameObject>(path: prefabPath);

			RegisterProgressWatchers(gameObject);
			return gameObject;
		}
	}
}
