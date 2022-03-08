using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Logic
{
	[RequireComponent(typeof(CanvasGroup))]
	public class LoadingCurtain : MonoBehaviour
	{
		private const float CURTAIN_WHEN_SHOWN_ALPHA = 1f;
		private const float CURTAIN_WHEN_HIDDEN_ALPHA = 0f;
		private const float FADE_IN_DURATION = 1f;

		private CanvasGroup _curtain;

		private void Awake()
		{
			DontDestroyOnLoad(this);
			_curtain = GetComponent<CanvasGroup>();
		}

		public void Show()
		{
			gameObject.SetActive(true);
			_curtain.alpha = CURTAIN_WHEN_SHOWN_ALPHA;
		}

		public void Hide()
		{
			_curtain.DOFade(CURTAIN_WHEN_HIDDEN_ALPHA, FADE_IN_DURATION).OnComplete(() => gameObject.SetActive(false));
		}
	}
}
