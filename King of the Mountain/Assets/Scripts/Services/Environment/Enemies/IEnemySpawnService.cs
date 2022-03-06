using Infrastructure.Services;

namespace Services.Environment.Enemies
{
	public interface IEnemySpawnService : IService
	{
		void StartSpawningEnemies();
		void StopSpawningEnemies();
	}
}
