using UnityEngine;

namespace Infrastructure.AssetManagement
{
	public class AssetProvider : IAssetProvider
	{
		public GameObject Instantiate(string path, Vector3 position)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, position, Quaternion.identity);
		}
		
		public GameObject Instantiate(string path, Vector3 position, Quaternion rotation)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, position, rotation);
		}

		public GameObject Instantiate(string path, Transform container)
		{
			GameObject prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, container);
		}

		public T Instantiate<T>(string path) where T : Object
		{
			T prefab = Resources.Load<T>(path);
			return Object.Instantiate(prefab);
		}
	}
}
