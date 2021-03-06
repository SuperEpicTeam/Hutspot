using System.Collections;
using UnityEngine;

namespace Hutspot.Minigames.HunebedGame
{
	public class HunebedGameManager : MonoBehaviour
	{
		public static HunebedGameManager Instance { get; private set; }

		public delegate void OnScoreIncrementEvent();

		public event OnScoreIncrementEvent OnScoreIncrement;

		public int Score { get; private set; }
		public float PreviousY { get; private set; }

		[SerializeField] private int _winCondition = 15;

		[SerializeField] private float _fallDistance = 5f;
		[SerializeField] private HunebedBehaviour _hunebedPrefab;
		[SerializeField] private DeathScreen _deathScreen;
		[SerializeField] private JsonReadWriteSystem _saveSystem;


		private HunebedBehaviour _currentHunebed;
		private HunebedBehaviour _previousHunebed;

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
			_previousHunebed = _currentHunebed;
			PreviousY = _previousHunebed != null ? _previousHunebed.transform.position.y : float.NegativeInfinity;

			StartCoroutine(MoveCamera());

			_currentHunebed = Instantiate(_hunebedPrefab, new Vector3(transform.position.x, yPos), Quaternion.identity);
			float hunebedScale = _previousHunebed != null ? GetHunebedWidth(_currentHunebed, _previousHunebed) : 1f;

			if (hunebedScale < 0.25f)
			{
				Destroy(_currentHunebed);
				OnDeathHandler();
			}
			else
			{
				_currentHunebed.transform.localScale = new Vector3(hunebedScale, 1f, 1f);

				_currentHunebed.OnLand += OnLandEventHandler;
				_currentHunebed.OnDie += OnDeathHandler;
			}
		}

		private IEnumerator MoveCamera()
		{
			float yPos = _currentHunebed != null ? _currentHunebed.transform.position.y + _fallDistance : 0f;
			yPos -= _fallDistance / 2f;

			float initialPos = Camera.main.transform.position.y;
			float lerpAlpha = 0f;

			while (Camera.main.transform.position.y < yPos)
			{
				float y = Mathf.Lerp(initialPos, yPos, lerpAlpha);
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, y, Camera.main.transform.position.z);
				lerpAlpha += Time.deltaTime;

				yield return new WaitForEndOfFrame();
			}

			yield return null;
		}

		private void OnDeathHandler()
		{
			const string highscoreSaveLocation = "HunebedHighScore";

			bool isHighscore = PlayerPrefs.GetInt(highscoreSaveLocation) < Score;
			string message = isHighscore ? "New highscore!" : "You died :(";

			if (Score >= _winCondition)
			{
				_saveSystem.SaveTrophies((int)TrophyEnum.Hunebed);
			}

			_deathScreen.Show(message, Score, isHighscore ? "" : $"Highscore: {PlayerPrefs.GetInt(highscoreSaveLocation)}");

			if(isHighscore)
			{
				PlayerPrefs.SetInt("HunebedHighScore", Score);
			}
		}

		private void OnLandEventHandler()
		{
			Score++;
			OnScoreIncrement?.Invoke();
			_currentHunebed.OnLand -= OnLandEventHandler;
			Start();
		}

		private float GetHunebedWidth(HunebedBehaviour hunebed, HunebedBehaviour previousHunebed)
		{
			float xBounds = previousHunebed.gameObject.GetComponent<BoxCollider2D>().bounds.size.x;
			float originalSize = hunebed.gameObject.GetComponent<BoxCollider2D>().bounds.size.x;

			return 1f / originalSize * xBounds;
		}
	}
}
