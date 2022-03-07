using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject _pauzeMenu;
	[SerializeField] private Button _openMenuButton;
	[SerializeField] private Button _closeMenuButton;
	[SerializeField] private Button _quitMinigameButton;

	private void Awake()
	{
		_openMenuButton.onClick.AddListener(OpenPauseMenu);
		_closeMenuButton.onClick.AddListener(ClosePauseMenu);
		_quitMinigameButton.onClick.AddListener(QuitMinigame);
	}

	private void OpenPauseMenu()
	{
		Time.timeScale = 0f;
		_pauzeMenu.SetActive(true);
	}

	private void ClosePauseMenu()
	{
		Time.timeScale = 1f;
		_pauzeMenu.SetActive(false);
	}

	private void QuitMinigame()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Map");
	}
}
