using System;
using System.Collections.Generic;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Logic;
using Logic.View;
using UnityEngine;

namespace Infrastructure.Factory
{
	public interface IGameFactory : IService
	{
		HudView CreateHud();
		List<ISavedProgressReader> ProgressReaders { get; }
		List<ISavedProgress> ProgressWriters { get; }
		Player Player { get; }
		GameObject FollowCamera { get; }
		HudView Hud { get; }
		void Cleanup();
		void RegisterProgressWatchers(GameObject gameObject);
		void Register(ISavedProgressReader progressReader);
		Player CreatePlayer(Vector3 position);
		Stair CreateStair(Vector3 position);
		GameObject CreateSwipeDetector();
		GameObject CreateFollowCamera(Transform target);
		Enemy CreateEnemy(Vector3 position);
	}
}
