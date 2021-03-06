using UnityEngine;
using UnityEngine.UI;
using Hutspot.Minigames;

public class HaringHappenManager : MonoBehaviour
{
	[SerializeField] private int _wincondition = 10;
	[SerializeField] private DeathScreen _deathScreen;
	[SerializeField] private JsonReadWriteSystem _saveSystem;

	public void GameOver(HaringHappenPlayer player)
	{
		Time.timeScale = 1f;

		_deathScreen.Show(player.GetPlayerPoints() >= _wincondition ? "Winner!" : "You lost! :(", player.GetPlayerPoints(), $"Highscore: {PlayerPrefs.GetInt("HaringHappenHighScore")}");

		if (player.GetPlayerPoints() >= _wincondition)
		{
			_saveSystem.SaveTrophies((int) TrophyEnum.Haringhappen);
		}

		CheckHighScore(player);
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
