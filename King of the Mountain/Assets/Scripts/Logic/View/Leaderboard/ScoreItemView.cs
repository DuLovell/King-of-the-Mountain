using TMPro;
using UnityEngine;

namespace Logic.View.Leaderboard
{
	public class ScoreItemView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _rankTextMesh;
		[SerializeField] private TextMeshProUGUI _scoreTextMesh;

		public void UpdateView(int rank, int score)
		{
			_rankTextMesh.text = $"{rank}.";
			_scoreTextMesh.text = score.ToString();
		}
	}
}
