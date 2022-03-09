using UnityEngine;

public class MissedObjectContainer : MonoBehaviour
{
	[SerializeField] private HaringHappenManager _haringHappenManager;
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
