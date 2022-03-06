using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Infrastructure.Factory
{
	public interface IGameFactory : IService
	{
		void CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		GameObject Player { get; }
		void Cleanup();
		void RegisterProgressWatchers(GameObject gameObject);
		void Register(ISavedProgressReader progressReader);
		GameObject CreatePlayer(Vector3 position);
		GameObject CreateStair(Vector3 position, Vector3 lookDirection);
		GameObject CreateSwipeDetector();
		GameObject CreateFollowCamera(Transform target);
	}
}
