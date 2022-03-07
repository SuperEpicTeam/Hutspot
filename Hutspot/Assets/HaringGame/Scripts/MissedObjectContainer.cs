using UnityEngine;

public class MissedObjectContainer : MonoBehaviour
{
	[SerializeField] private HaringHappenTimer _timer;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Haring>())
		{
			_timer.ReduceTime(5);
		}
		Destroy(other.gameObject);
	}
}
