using System;
using Infrastructure.Services;
using Services.Environment.Stairs;
using TMPro;
using UnityEngine;

namespace Logic.View
{
	public class StairCounterView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textMesh;
		
		private IStairsCountService _stairsCountService;

		private void Awake()
		{
			_stairsCountService = AllServices.Container.Single<IStairsCountService>();
		}

		private void OnEnable()
		{
			_stairsCountService.OnPlayerStairPositionChanged += UpdateView;
		}

		private void OnDisable()
		{
			_stairsCountService.OnPlayerStairPositionChanged -= UpdateView;
		}

		private void UpdateView(int playerCurrentStairPositionNumber)
		{
			_textMesh.text = playerCurrentStairPositionNumber.ToString();
		}
	}
}
