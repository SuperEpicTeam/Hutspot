using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class JsonReadWriteSystem : MonoBehaviour
{
	[SerializeField] private Stamp[] _stamps;
	[SerializeField] private GameObject _stampBook;

	[SerializeField] private Button _toggleButton;

	private List<int> _listToArrayConverter;

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
		StampData saveDataNew = new StampData();

		string jsonOld = File.ReadAllText(Application.dataPath + "/StampSystem/Scripts/PlayerStampDataFile.json");
		StampData saveDataOld = JsonUtility.FromJson<StampData>(jsonOld);
		_listToArrayConverter = new List<int>();

		for (int i = 0; i < saveDataOld._collectedStamps.Length; i++)
		{
			if (saveDataOld._collectedStamps[i] == thropyIndex)
			{
				return;
			}
			else
			{
				_listToArrayConverter.Add(saveDataOld._collectedStamps[i]);
			}
		}

		_listToArrayConverter.Add(thropyIndex);

		saveDataNew._collectedStamps = null;
		saveDataNew._collectedStamps = _listToArrayConverter.ToArray();

		string jsonNew = JsonUtility.ToJson(saveDataNew, true);
		File.WriteAllText(Application.dataPath + "/StampSystem/Scripts/PlayerStampDataFile.json", jsonNew);
	}

	/// <summary>
	/// Load stamp data and check if the player has collected a stamp.
	/// </summary>
	private void LoadStamps()
	{
		string json = File.ReadAllText(Application.dataPath + "/StampSystem/Scripts/PlayerStampDataFile.json");
		StampData saveData = JsonUtility.FromJson<StampData>(json);

		for (int i = 0; i < _stamps.Length; i++)
		{
			for (int j = 0; j < saveData._collectedStamps.Length; j++)
			{
				if (_stamps[i].GetStampID() == saveData._collectedStamps[j])
				{
					_stamps[i].ShowStamp();
				}
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
