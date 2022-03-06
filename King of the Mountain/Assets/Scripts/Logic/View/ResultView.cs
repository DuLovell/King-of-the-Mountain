using System;
using Infrastructure.Services;
using Services.Environment.Stairs;
using TMPro;
using UnityEngine;

namespace Logic.View
{
	public class ResultView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _textMesh;
		
		private IStairsCountService _stairsCountService;

		private void Awake()
		{
			_stairsCountService = AllServices.Container.Single<IStairsCountService>();
		}

		private void OnEnable()
		{
			_textMesh.text = _stairsCountService.LastPlayerPositionStairNumber.ToString();
		}
	}
}
