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
		private const int SPAWN_STAIRS_OFFSET = 20;
		
		private readonly IStairsCountService _stairCountService;
		private readonly IGameFactory _gameFactory;
		private readonly ICoroutineRunner _coroutineRunner;

		private readonly Queue<Enemy> _activeEnemies;
		private Coroutine _enemySpawnCoroutine;

		private float NextSpawnDelay => Random.Range(1f, 3f);

		public EnemySpawnService(IStairsCountService stairCountService, IGameFactory gameFactory,
			ICoroutineRunner coroutineRunner)
		{
			_stairCountService = stairCountService;
			_gameFactory = gameFactory;
			_activeEnemies = new Queue<Enemy>();
			_coroutineRunner = coroutineRunner;
		}
		
		public void StartSpawningEnemies()
		{
			_enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemyRoutine());

			IEnumerator SpawnEnemyRoutine()
			{
				while (true)
				{
					Vector3 spawnPosition = (_stairCountService.LastPlayerPositionStairNumber + SPAWN_STAIRS_OFFSET) * Config.StairOffset;
					SpawnEnemy(spawnPosition);
					
					yield return new WaitForSeconds(NextSpawnDelay);
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
			Enemy enemy = _gameFactory.CreateEnemy(spawnPosition);
			enemy.VisibilityReporter.OnBecomeInvisible += HideEnemy;
			_activeEnemies.Enqueue(enemy);
		}

		private void HideEnemy()
		{
			Enemy enemyToHide = _activeEnemies.Dequeue();
			enemyToHide.VisibilityReporter.OnBecomeInvisible -= HideEnemy;
			Object.Destroy(enemyToHide);
		}
	}
}
