using System.Collections;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(CanvasGroup))]
	//TODO Заменить на реализацию через DOTween
	public class LoadingCurtain : MonoBehaviour
	{
		private CanvasGroup _curtain;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			_curtain = GetComponent<CanvasGroup>();
		}

		public void Show()
		{
			gameObject.SetActive(true);
			_curtain.alpha = 1;
		}

		public void Hide() => StartCoroutine(DoFadeIn());

		private IEnumerator DoFadeIn()
		{
			while (_curtain.alpha > 0)
			{
				_curtain.alpha -= 0.03f;
				yield return new WaitForSeconds(0.03f);
			}

			gameObject.SetActive(false);
		}
	}
}
