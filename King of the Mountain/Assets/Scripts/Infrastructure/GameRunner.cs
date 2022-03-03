using UnityEngine;

namespace Infrastructure
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField] private GameBootstrapper BootstrapperPrefab;

		private void Awake()
		{
			GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

			if (bootstrapper != null) return;

			Instantiate(BootstrapperPrefab);
		}
	}
}
