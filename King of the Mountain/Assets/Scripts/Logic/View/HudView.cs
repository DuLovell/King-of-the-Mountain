using System;
using Logic.View.Screens;
using UnityEditor;
using UnityEngine;

namespace Logic.View
{
	public class HudView : MonoBehaviour
	{
		[SerializeField] private HelloScreen _helloScreen;
		[SerializeField] private StartScreen _startScreen;
		[SerializeField] private GameplayScreen _gameplayScreen;
		[SerializeField] private GameOverScreen _gameOverScreen;

		private MenuScreen _currentScreen;

		public HelloScreen ShowHelloScreen()
		{
			SetCurrentScreen(_helloScreen);
			return _helloScreen;
		}
		
		public StartScreen ShowStartScreen()
		{
			SetCurrentScreen(_startScreen);
			return _startScreen;
		}

		public GameplayScreen ShowGameplayScreen()
		{
			SetCurrentScreen(_gameplayScreen);
			return _gameplayScreen;
		}
		
		public GameOverScreen ShowGameOverScreen()
		{
			SetCurrentScreen(_gameOverScreen);
			return _gameOverScreen;
		}

		private void SetCurrentScreen<TScreen>(TScreen screen) where TScreen : MenuScreen
		{
			_currentScreen?.Hide();
			_currentScreen = screen;
			_currentScreen.Show();
		}
	}
}
