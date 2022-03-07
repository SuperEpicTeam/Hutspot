using UnityEngine;
using UnityEngine.UI;

public class HaringHappenPlayer : MonoBehaviour
{
	private int _points;

	[SerializeField] private Text _scoreText;
	[SerializeField] private HaringHappenTimer _timer;

	private void OnTriggerEnter(Collider other)
	{
		if (_timer.GetTimerActivity())
		{
			if (other.gameObject.GetComponent<Haring>())
			{
				_points++;
				_scoreText.text = "Score: " + _points.ToString();
				Destroy(other.gameObject);
			}
			else
			{
				_timer.ReduceTime(5);
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
}
