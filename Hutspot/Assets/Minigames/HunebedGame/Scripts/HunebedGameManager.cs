using UnityEngine;

namespace Hutspot.Minigames.HunebedGame
{
	public class HunebedGameManager : MonoBehaviour
	{
		public static HunebedGameManager Instance { get; private set; }
		
		public int Score { get; private set; }

		[SerializeField] private HunebedBehaviour _hunebedPrefab;
		private HunebedBehaviour _currentHunebed;

		private void Awake()
		{
			if(Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(this);
			}
		}

		private void Start()
		{
			_currentHunebed = Instantiate(_hunebedPrefab, transform.position, Quaternion.identity);
			_currentHunebed.OnLand += OnLand;
		}
		
		private void OnLand()
		{
			_currentHunebed.OnLand -= OnLand;
			Start();
		}
	}
}
