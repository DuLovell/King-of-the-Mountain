using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.View.Screens
{
	public class HelloScreen : MenuScreen
	{
		public event Action<string> OnNameEntered;

		[SerializeField] private TMP_InputField _nameInputField;
		[SerializeField] private Button _submitNameButton;

		private void OnEnable()
		{
			_submitNameButton.onClick.AddListener(InvokeOnNameEntered);
		}

		private void OnDisable()
		{
			_submitNameButton.onClick.RemoveListener(InvokeOnNameEntered);
		}

		private void InvokeOnNameEntered()
		{
			OnNameEntered?.Invoke(_nameInputField.text);
		}
	}
}
