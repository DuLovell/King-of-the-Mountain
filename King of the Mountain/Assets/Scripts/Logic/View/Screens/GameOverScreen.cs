using System;
using DG.Tweening;
using Logic.View.Leaderboard;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.View.Screens
{
	public class GameOverScreen : MenuScreen
	{
		public event Action OnGameOverPressed;
		
		[Header("Background Settings")]
		[SerializeField] private Image _backgroundImage;
		[SerializeField] private float _backgroundAlphaEndValue = 1f;
		[SerializeField] private float _fadeOutDuration = 1f;

		[Header("Other UI")] 
		[SerializeField] private ResultView _resultView;
		[SerializeField] private Button _playerAgainButton;
		[SerializeField] private LeaderboardView _leaderboardView;
		
		public override void Show()
		{
			base.Show();
			_backgroundImage.DOFade(_backgroundAlphaEndValue, _fadeOutDuration).OnComplete(ShowUiElements);
		}

		private void OnEnable()
		{
			_playerAgainButton.onClick.AddListener(InvokeOnGameOverPressed);
		}

		private void OnDisable()
		{
			_playerAgainButton.onClick.RemoveListener(InvokeOnGameOverPressed);
		}

		private void ShowUiElements()
		{
			_resultView.gameObject.SetActive(true);
			_playerAgainButton.gameObject.SetActive(true);
			_leaderboardView.gameObject.SetActive(true);
		}

		private void InvokeOnGameOverPressed()
		{
			OnGameOverPressed?.Invoke();
		}
	}
}
