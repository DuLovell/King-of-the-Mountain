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
		private const string ENEMIES_CONTAINER_NAME = "Enemies";

		private readonly IStairsCountService _stairCountService;
		private readonly ICoroutineRunner _coroutineRunner;

		private readonly Vector3 _stairOffset = Config.StairOffset;
		private readonly MonoPool<Enemy> _enemiesPool;

		private Coroutine _enemySpawnCoroutine;
		private Transform _enemiesContainer;

		private float NextSpawnDelay => Random.Range(1f, 3f);
		private float InsideStairsRandomPositionX => Random.Range(-Config.STAIR_LENGTH / 2, Config.STAIR_LENGTH / 2);


		public EnemySpawnService(IStairsCountService stairCountService, IGameFactory gameFactory,
			ICoroutineRunner coroutineRunner)
		{
			_stairCountService = stairCountService;
			_coroutineRunner = coroutineRunner;

			_enemiesPool = new MonoPool<Enemy>(
				createFunction: () => gameFactory.CreateEnemy(Vector3.zero),
				actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
				actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
				actionOnDestroy: Object.Destroy, 
				defaultCapacity: 10,
				autoExpand: true);
		}

		public void StartSpawningEnemies()
		{
			_enemySpawnCoroutine = _coroutineRunner.StartCoroutine(SpawnEnemyRoutine());

			IEnumerator SpawnEnemyRoutine()
			{
				_enemiesContainer = new GameObject(ENEMIES_CONTAINER_NAME).transform;
				_enemiesPool.CreatePool(_enemiesContainer);
				
				while (true)
				{
					Vector3 spawnPosition = GetSpawnPosition();
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

		private Vector3 GetSpawnPosition()
		{
			Vector3 spawnPosition = (_stairCountService.LastPlayerPositionStairNumber + SPAWN_STAIRS_OFFSET) * _stairOffset;
			spawnPosition.x = InsideStairsRandomPositionX;
			return spawnPosition;
		}

		private void SpawnEnemy(Vector3 spawnPosition)
		{
			Enemy enemy = _enemiesPool.GetElement();
			enemy.StartMoving(spawnPosition);
			enemy.OnBecameInvisible += HideEnemy;
		}

		private void HideEnemy(Enemy enemy)
		{
			enemy.OnBecameInvisible -= HideEnemy;
			enemy.StopMoving();
			_enemiesPool.ReleaseElement(enemy);
		}
	}
}
