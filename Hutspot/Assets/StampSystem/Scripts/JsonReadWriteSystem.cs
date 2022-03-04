using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class JsonReadWriteSystem : MonoBehaviour
{
	[SerializeField] private Throphy[] _trophies;
	[SerializeField] private GameObject _trophyBook;

	[SerializeField] private Button _activationButton;
	[SerializeField] private Button _deactivationButton;

	private List<int> _listToArrayConverter;

	private void Awake()
	{
		_activationButton.onClick.AddListener(ShowTrophyBook);
		_deactivationButton.onClick.AddListener(HideTrophyBook);
	}

	/// <summary>
	/// Save trophies to the PlayerTrophyDataFile.json file
	/// </summary>
	public void SaveTrophies(int thropyIndex)
	{
		TrophyData saveDataNew = new TrophyData();

		string jsonOld = File.ReadAllText(Application.dataPath + "/StampSystem/Scripts/PlayerTrophyDataFile.json");
		TrophyData saveDataOld = JsonUtility.FromJson<TrophyData>(jsonOld);
		_listToArrayConverter = new List<int>();

		for (int i = 0; i < saveDataOld.GetCollectedTrophies().Length; i++)
		{
			if (saveDataOld.GetCollectedTrophies()[i] == thropyIndex)
			{
				return;
			}
			else
			{
				_listToArrayConverter.Add(saveDataOld.GetCollectedTrophies()[i]);
			}
		}

		_listToArrayConverter.Add(thropyIndex);
		saveDataNew.SetCollectedTrophies(null);
		saveDataNew.SetCollectedTrophies(_listToArrayConverter.ToArray());

		string jsonNew = JsonUtility.ToJson(saveDataNew, true);
		File.WriteAllText(Application.dataPath + "/StampSystem/Scripts/PlayerTrophyDataFile.json", jsonNew);
	}

	/// <summary>
	/// Load trophy data and check if the player has collected a trophy.
	/// </summary>
	private void LoadTrophies()
	{
		string json = File.ReadAllText(Application.dataPath + "/StampSystem/Scripts/PlayerTrophyDataFile.json");
		TrophyData saveData = JsonUtility.FromJson<TrophyData>(json);

		for (int i = 0; i < _trophies.Length; i++)
		{
			for (int j = 0; j < saveData.GetCollectedTrophies().Length; j++)
			{
				if (_trophies[i].GetTrophyID() == saveData.GetCollectedTrophies()[j])
				{
					_trophies[i].ShowTrophy();
				}
			}
		}
	}

	private void ToggleTrophyBook(bool showBook)
	{
		GameObject.Find("Canvas").SetActive(showBook);
		if (showBook) LoadTrophies();
	}

	private void ShowTrophyBook()
	{
		LoadTrophies();
		_trophyBook.SetActive(true);
	}
	private void HideTrophyBook()
	{
		_trophyBook.SetActive(false);
	}
}
