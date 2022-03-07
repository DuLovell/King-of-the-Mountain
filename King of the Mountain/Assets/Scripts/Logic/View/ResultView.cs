using System;
using Data;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using Services.Environment.Stairs;
using TMPro;
using UnityEngine;

namespace Logic.View
{
	public class ResultView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _curentResultTextMesh;
		[SerializeField] private TextMeshProUGUI _bestResultTextMesh;
		
		private IStairsCountService _stairsCountService;

		private void Awake()
		{
			_stairsCountService = AllServices.Container.Single<IStairsCountService>();
		}

		private void OnEnable()
		{
			_curentResultTextMesh.text = _stairsCountService.LastPlayerPositionStairNumber.ToString();
			_bestResultTextMesh.text = _stairsCountService.BestPlayerPositionStairNumber.ToString();
		}

		public void LoadProgress(PlayerProgress progress)
		{
			_bestResultTextMesh.text = progress.BestScore.ToString();
		}

		public void UpdateProgress(PlayerProgress progress)
		{
			progress.BestScore = _stairsCountService.LastPlayerPositionStairNumber;
		}
	}
}
