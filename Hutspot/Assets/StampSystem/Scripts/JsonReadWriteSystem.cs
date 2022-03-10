using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class JsonReadWriteSystem : MonoBehaviour
{
	[SerializeField] private Stamp[] _stamps;
	[SerializeField] private GameObject _stampBook;

	[SerializeField] private Button _toggleButton;

	private void Awake()
	{
		if(_toggleButton != null){
			_toggleButton.onClick.AddListener(ToggleStampBook);
		}
	}

	/// <summary>
	/// Save stamps to the PlayerStampDataFile.json file
	/// </summary>
	public void SaveTrophies(int thropyIndex)
	{
		PlayerPrefs.SetInt($"trophy{thropyIndex}", 0);
	}

	/// <summary>
	/// Load stamp data and check if the player has collected a stamp.
	/// </summary>
	private void LoadStamps()
	{
		for (int i = 0; i < _stamps.Length; i++)
		{
			if(PlayerPrefs.HasKey($"trophy{i}"))
			{
				_stamps[i].ShowStamp();
			}
		}
	}

	private void ToggleStampBook()
	{
		if(!_stampBook.activeSelf)
		{
			ShowStampBook();
		}
		else
		{
			HideStampBook();
		}
	}

	private void ShowStampBook()
	{
		LoadStamps();
		_stampBook.SetActive(true);
	}
	private void HideStampBook()
	{
		_stampBook.SetActive(false);
	}
}
