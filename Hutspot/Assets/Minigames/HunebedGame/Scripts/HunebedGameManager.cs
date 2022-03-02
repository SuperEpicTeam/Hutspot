using UnityEngine;

namespace Hutspot.Minigames.HunebedGame
{
	public class HunebedGameManager : MonoBehaviour
	{
		public static HunebedGameManager Instance { get; private set; }
		
		public int Score { get; private set; }

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

		private void Update()
		{

		}
	}
}
