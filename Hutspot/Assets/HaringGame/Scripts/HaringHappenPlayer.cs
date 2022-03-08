﻿using UnityEngine;
using UnityEngine.UI;

public class HaringHappenPlayer : MonoBehaviour
{
	private int _points;
	private int _health = 3;

	[SerializeField] private Text _scoreText;
	[SerializeField] private HaringHappenTimer _timer;
	[SerializeField] private GameObject _healtBar;
	[SerializeField] private Image[] _healthImages;

	private void OnTriggerEnter(Collider other)
	{
			/*if (_timer.GetTimerActivity())*/ if (_health > 0)
			{
			if (other.gameObject.GetComponent<Haring>())
			{
				_points++;
				_scoreText.text = "Score: " + _points.ToString();
				Destroy(other.gameObject);
			}
			else
			{
				PlayerDamage();
				Destroy(other.gameObject);
			}
		}
		else
		{
			Destroy(other.gameObject);
		}
	}

	public int GetPlayerPoints()
	{
		return _points;
	}

	public void PlayerDamage()
	{
		_health -= 1;
		if (_health > 0)
		{
			for (int i = _health; i < _healthImages.Length; i++)
			{
				_healthImages[i].enabled = false;
			}
		}
		else
		{
			_healthImages[0].enabled = false;
			StartCoroutine(_timer.GameOver(gameObject.GetComponent<HaringHappenPlayer>()));
		}
	}
}
