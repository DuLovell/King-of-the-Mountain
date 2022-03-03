using UnityEngine;

namespace Infrastructure.AssetManagement
{
	public class AssetProvider : IAssetProvider
	{
		public GameObject Instantiate(string path, Vector3 position)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, position, Quaternion.identity);
		}
		
		public GameObject Instantiate(string path, Vector3 position, Quaternion rotation)
		{
			var prefab = Resources.Load<GameObject>(path);
			return Object.Instantiate(prefab, position, rotation);
		}

		public T Instantiate<T>(string path) where T : Object
		{
			T prefab = Resources.Load<T>(path);
			return Object.Instantiate(prefab);
		}
	}
}
