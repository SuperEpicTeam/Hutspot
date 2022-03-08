using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Hutspot.Minigames
{
	[RequireComponent(typeof(RectTransform))]
	public class DeathScreen : MonoBehaviour
	{
		[SerializeField] private string _mainSceneName;

		[Header("Output")]
		[SerializeField] private TextMeshProUGUI _messageText;
		[SerializeField] private TextMeshProUGUI _scoreText;
		[SerializeField] private TextMeshProUGUI _additionalInformation;

		[Header("Buttons")]
		[SerializeField] private Button _retryButton;
		[SerializeField] private Button _continueButton;

		private void Awake()
		{
			_retryButton.onClick.AddListener(OnRetry);
			_continueButton.onClick.AddListener(OnContinue);
		}

		public void Show(string message, int score, string additionalInformation)
		{
			_messageText.text = message;
			_scoreText.text = $"{score}";
			_additionalInformation.text = additionalInformation;

			gameObject.SetActive(true);
		}

		private void OnRetry()
		{
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}

		private void OnContinue()
		{
			SceneManager.LoadScene(_mainSceneName);
		}
	}
}