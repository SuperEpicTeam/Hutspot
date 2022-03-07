using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HaringHappenTimer : MonoBehaviour
{
	private float _time = 30f;
	[SerializeField] private Text _timerText;
	[SerializeField] private Text _gameOverText;
	[SerializeField] private Text _highScoreText;
	[SerializeField] private HaringHappenPlayer _player;
	[SerializeField] private int _wincondition = 10;

	private bool _timerIsActive = true;

	void Start()
	{
		StartCoroutine(MainTick());
	}

	/// <summary>
	/// Timer countdown per second.
	/// </summary>
	/// <returns></returns>
	IEnumerator MainTick()
	{
		yield return new WaitForSeconds(1);

		if (_timerIsActive)
		{
			ReduceTime(1);
			StartCoroutine(MainTick());
		}
	}

	/// <summary>
	/// Reduce timer with float parameter. If time is up, show stats and stop the game.
	/// </summary>
	/// <param name="time"></param>
	public void ReduceTime(float time)
	{
		if ((_time -= time) > 0)
		{
			_time -= time;
			_timerText.text = _time.ToString();
		}
		else
		{
			_timerIsActive = false;
			_gameOverText.enabled = true;
			_highScoreText.enabled = true;
			_timerText.enabled = false;
			StartCoroutine(GameOver());
		}
	}

	IEnumerator GameOver()
	{
		if (_player.GetPlayerPoints() >= _wincondition)
		{
			//doe hier de gekke trophy functionalteit
			_gameOverText.text = "Winner!";
		}

		CheckHighScore();
		_highScoreText.text = "Highscore" + PlayerPrefs.GetInt("HaringHappenHighScore").ToString();

		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("Map");
	}

	/// <summary>
	/// Check if the timer is active.
	/// </summary>
	/// <returns>_timerIsActive</returns>
	public bool GetTimerActivity()
	{
		return _timerIsActive;
	}

	/// <summary>
	/// Check if the player score is a new highscore. If this is the case, update the playerprefs with the new highscore (saving system).
	/// </summary>
	private void CheckHighScore()
	{
		if (_player.GetPlayerPoints() > PlayerPrefs.GetInt("HaringHappenHighScore"))
		{
			PlayerPrefs.SetInt("HaringHappenHighScore", _player.GetPlayerPoints());
		}
	}
}
