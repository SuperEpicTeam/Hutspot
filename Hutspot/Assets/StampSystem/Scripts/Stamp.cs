using UnityEngine;
using UnityEngine.UI;

public class Stamp : MonoBehaviour
{
	[SerializeField] private int _stampId;
	[SerializeField] private Image stampImage;

	public void ShowStamp()
	{
		stampImage.color = Color.green;
	}

	public int GetStampID()
	{
		return _stampId;
	}
}
