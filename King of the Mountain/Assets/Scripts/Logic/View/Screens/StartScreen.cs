using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.View.Screens
{
	public class StartScreen : MenuScreen
	{
		public event Action OnGameStarted;

		[SerializeField] private Button _gameStartButton;

		private void OnEnable()
		{
			_gameStartButton.onClick.AddListener(InvokeOnGameStarted);
		}

		private void OnDisable()
		{
			_gameStartButton.onClick.RemoveListener(InvokeOnGameStarted);
		}

		private void InvokeOnGameStarted()
		{
			OnGameStarted?.Invoke();
		}
	}
}
