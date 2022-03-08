using UnityEngine;

namespace Hutspot.Minigames.HunebedGame
{
	public class HunebedGameManager : MonoBehaviour
	{
		public static HunebedGameManager Instance { get; private set; }
		
		public int Score { get; private set; }

		[SerializeField] private float _fallDistance = 5f;
		[SerializeField] private HunebedBehaviour _hunebedPrefab;
		
		private HunebedBehaviour _currentHunebed;
		private float _hunebedScale = 1f;

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
			float yPos = _currentHunebed != null ? _currentHunebed.transform.position.y + _fallDistance : 0f;
			HunebedBehaviour previousHunebed = _currentHunebed;

			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, yPos - _fallDistance / 2f, Camera.main.transform.position.z);
			_currentHunebed = Instantiate(_hunebedPrefab, Vector3.up * yPos, Quaternion.identity);
			_hunebedScale = previousHunebed != null ? GetHunebedWidth(_currentHunebed, previousHunebed) : _hunebedScale;
			_currentHunebed.transform.localScale = new Vector3(_hunebedScale, 1f, 1f);

			_currentHunebed.OnLand += OnLand;
		}
		
		private void OnLand()
		{
			_currentHunebed.OnLand -= OnLand;
			Score++;
			Start();
		}

		private float GetHunebedWidth(HunebedBehaviour hunebed, HunebedBehaviour previousHunebed)
		{
			float xBounds = previousHunebed.gameObject.GetComponent<BoxCollider2D>().bounds.size.x;
			float originalSize = hunebed.gameObject.GetComponent<BoxCollider2D>().bounds.size.x;

			//TODO return value between 0 - 1 as hunebed scale
			return 1f / originalSize * xBounds;
		}
	}
}
