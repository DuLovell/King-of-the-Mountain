using System.Collections;
using System.Collections.Generic;
using Data;
using Infrastructure;
using Infrastructure.Factory;
using Logic;
using Services.Environment.Stairs;
using UnityEngine;

namespace Services.Environment.Enemies
{
	public class EnemySpawnService : IEnemySpawnService
	{
		private readonly IStairsCountService _stairCountService;
		private readonly IGameFactory _gameFactory;
		private readonly ICoroutineRunner _coroutineRunner;

		private readonly Queue<GameObject> _activeEnemies;
		private Coroutine _enemySpawnCoroutine;

		public EnemySpawnService(IStairsCountService stairCountService, IGameFactory gameFactory,
			ICoroutineRunner coroutineRunner)
		{
			_stairCountService = stairCountService;
			_gameFactory = gameFactory;
			_activeEnemies = new Queue<GameObject>();
			_coroutineRunner = coroutineRunner;
		}
		
		public void StartSpawningEnemies()
		{
			_enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemyRoutine());

			IEnumerator SpawnEnemyRoutine()
			{
				while (true)
				{
					Vector3 spawnPosition = (_stairCountService.LastPlayerPositionStairNumber + 20) * Config.StairOffset;
					SpawnEnemy(spawnPosition);
					
					yield return new WaitForSeconds(2f);
				}
			}
		}

		public void StopSpawningEnemies()
		{
			if (_enemySpawnCoroutine == null) return;
			
			_coroutineRunner.StopCoroutine(_enemySpawnCoroutine);
			_enemySpawnCoroutine = null;
		}

		private void SpawnEnemy(Vector3 spawnPosition)
		{
			GameObject enemy = _gameFactory.CreateEnemy(spawnPosition);
			enemy.GetComponent<RendererVisibilityReporter>().OnBecomeInvisible += HideEnemy;
			_activeEnemies.Enqueue(enemy);
		}

		private void HideEnemy()
		{
			GameObject enemyToHide = _activeEnemies.Dequeue();
			enemyToHide.GetComponent<RendererVisibilityReporter>().OnBecomeInvisible -= HideEnemy;
			Object.Destroy(enemyToHide);
		}
	}
}
