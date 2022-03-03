using Infrastructure.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Infrastructure.AssetManagement
{
	public interface IAssetProvider : IService
	{
		T Instantiate<T>(string path) where T : Object;
		GameObject Instantiate(string path, Vector3 position);
		GameObject Instantiate(string path, Vector3 position, Quaternion rotation);
	}
}
