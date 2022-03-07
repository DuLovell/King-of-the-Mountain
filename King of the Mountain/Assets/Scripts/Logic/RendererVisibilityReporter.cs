using System;
using UnityEngine;

namespace Logic
{
	public class RendererVisibilityReporter : MonoBehaviour
	{
		public event Action OnBecomeInvisible;
		public event Action OnBecomeVisible;

		[SerializeField] private Renderer _renderer;
		
		private bool _wasVisible;

		private void LateUpdate()
		{
			CheckIfVisibilityStatusChanged();

			_wasVisible = _renderer.isVisible;
		}

		private void CheckIfVisibilityStatusChanged()
		{
			if (_renderer.isVisible && !_wasVisible)
			{
				OnBecomeVisible?.Invoke();
			}

			if (!_renderer.isVisible && _wasVisible)
			{
				OnBecomeInvisible?.Invoke();
			}
		}
	}
}
