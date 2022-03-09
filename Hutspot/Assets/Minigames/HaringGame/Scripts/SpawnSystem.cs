using System.Collections;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
	[SerializeField] private Vector3 _center;
	[SerializeField] private Vector3 _size;

	[SerializeField] private GameObject _haringPrefab;
	[SerializeField] private GameObject _hutspotprefab;

	void Start()
	{
		StartCoroutine(mainTick());
	}

	IEnumerator mainTick()
	{
		yield return new WaitForSeconds(0.5f);

		if (Random.Range(0f, 100f) > 75f)
		{
			SpawnObjects(_hutspotprefab);
		}
		else
		{
			SpawnObjects(_haringPrefab);
		}

		StartCoroutine(mainTick());
	}

	public void SpawnObjects(GameObject objectToSpawn)
	{
		Vector3 pos = _center + new Vector3(Random.Range(-_size.x / 2, _size.x / 2), Random.Range(-_size.y / 2, _size.y / 2), 0);

		Instantiate(objectToSpawn, pos, Quaternion.identity);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0, 0, 1f);
		Gizmos.DrawCube(_center, _size);
	}
}
