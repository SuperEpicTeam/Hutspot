using UnityEngine;

namespace Hutspot.Minigames.HunebedGame
{
	public class HunebedBehaviour : MonoBehaviour
	{
		[SerializeField] private Transform[] _verticleStones = new Transform[2];
		[SerializeField] private Texture2D _hunebedStoneSprite;

		private float _levelWidth;
		private Vector2 _direction;
		private bool _isStatic;

		private void Awake()
		{
			foreach(Transform stone in _verticleStones)
			{
				stone.gameObject.SetActive(false);
			}
		}

		private void Start()
		{
			_levelWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		}

		private void Update()
		{
			const int minimumSpeed = 1;
			const float widthMargin = 0f;

			if(!_isStatic)
			{
				//Get the camera bounds to check if we have to flip our velocity
				_direction = (_direction == Vector2.left && transform.position.x > -_levelWidth + widthMargin)
							|| transform.position.x > _levelWidth - widthMargin
							? Vector2.left : Vector2.right;

				//move the stone left - right, make the velocity based on the score
				transform.Translate(_direction * (minimumSpeed + HunebedGameManager.Instance.Score) * Time.deltaTime);

				//TODO: Handle input
				if (Input.GetMouseButton(0))
				{
					GetComponent<SpriteRenderer>().sprite = Sprite.Create(_hunebedStoneSprite, new Rect(100f, 0.0f, _hunebedStoneSprite.width - 200f, _hunebedStoneSprite.height), Vector2.one / 2, 100f);

					//gameObject.AddComponent<Rigidbody2D>();
					_isStatic = true;
				}
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			//TODO: Make this stone static
			//TODO: Make the parts of the stone outside the bounds break off
			//TODO: Show the verticle stones (animation?)
		}
	}
}
