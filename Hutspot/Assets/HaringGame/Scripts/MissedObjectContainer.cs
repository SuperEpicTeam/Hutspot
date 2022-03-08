using UnityEngine;

public class MissedObjectContainer : MonoBehaviour
{
	[SerializeField] private HaringHappenTimer _timer;
	[SerializeField] private HaringHappenPlayer _player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Haring>())
		{
			_player.PlayerDamage();
		}
		Destroy(other.gameObject);
	}
}
