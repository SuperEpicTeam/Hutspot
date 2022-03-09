using UnityEngine;
using TMPro;

namespace Hutspot.Minigames.HunebedGame
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class ScoreVisualizer : MonoBehaviour
	{
		private TextMeshProUGUI _text;

		private void Awake()
		{
			_text = transform.GetComponent<TextMeshProUGUI>();
		}

		private void Start()
		{
			HunebedGameManager.Instance.OnScoreIncrement += OnScoreIncrement;
		}

		private void OnScoreIncrement()
		{
			_text.text = $"{HunebedGameManager.Instance.Score}";
		}
	}
}
