using UnityEngine;
using UnityEngine.UI;

public class Stamp : MonoBehaviour
{
	[SerializeField] private int _stampId;
	[SerializeField] private Image _stampImage;

	public void ShowStamp()
	{
		_stampImage.enabled = true;
	}

	public int GetStampID()
	{
		return _stampId;
	}
}
