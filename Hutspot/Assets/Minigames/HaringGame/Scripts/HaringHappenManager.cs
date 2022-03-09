using UnityEngine;
using UnityEngine.UI;
using Hutspot.Minigames;

public class HaringHappenManager : MonoBehaviour
{
	[SerializeField] private Text _gameOverText;
	[SerializeField] private Text _highScoreText;
	[SerializeField] private int _wincondition = 10;
	[SerializeField] private DeathScreen _deathScreen;

	public void GameOver(HaringHappenPlayer player)
	{
		//_gameOverText.enabled = true;
		//_highScoreText.enabled = true;

		Time.timeScale = 1f;

		_deathScreen.Show(player.GetPlayerPoints() >= _wincondition ? "Winner!" : "You lost! :(", player.GetPlayerPoints(), $"Highscore: {PlayerPrefs.GetInt("HaringHappenHighScore")}");

		CheckHighScore(player);
		//_highScoreText.text = "Highscore" + PlayerPrefs.GetInt("HaringHappenHighScore").ToString();
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
