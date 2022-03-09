using UnityEngine;
using UnityEngine.UI;

public class HaringHappenManager : MonoBehaviour
{
	[SerializeField] private Text _gameOverText;
	[SerializeField] private Text _highScoreText;
	[SerializeField] private int _wincondition = 10;

	public void GameOver(HaringHappenPlayer player)
	{
		_gameOverText.enabled = true;
		_highScoreText.enabled = true;

		Time.timeScale = 1f;

		if (player.GetPlayerPoints() >= _wincondition)
		{
			//doe hier de gekke trophy functionalteit
			_gameOverText.text = "Winner!";
		}

		CheckHighScore(player);
		_highScoreText.text = "Highscore" + PlayerPrefs.GetInt("HaringHappenHighScore").ToString();
	}

	/// <summary>
	/// Check if the player score is a new highscore. If this is the case, update the playerprefs with the new highscore (saving system).
	/// </summary>
	private void CheckHighScore( HaringHappenPlayer player)
	{
		if (player.GetPlayerPoints() > PlayerPrefs.GetInt("HaringHappenHighScore"))
		{
			PlayerPrefs.SetInt("HaringHappenHighScore", player.GetPlayerPoints());
		}
	}
}
