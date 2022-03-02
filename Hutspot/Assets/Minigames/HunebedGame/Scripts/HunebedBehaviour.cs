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
		private float _pixelSizeWorldSpace;

		private BoxCollider2D _collider;

		public delegate void OnLandEvent();

		public event OnLandEvent OnLand;
		public event OnLandEvent OnDie;

		private void Awake()
		{
			_collider = transform.GetComponent<BoxCollider2D>();

			foreach (Transform stone in _verticleStones)
			{
				stone.gameObject.SetActive(false);
			}
		}

		private void Start()
		{
			_levelWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
			_pixelSizeWorldSpace = _collider.bounds.size.x / _hunebedStoneSprite.width;
		}

		private void Update()
		{
			const int minimumSpeed = 1;
			const float widthMargin = 0f;

			if(!_isStatic)
			{
				/*
				//Get the camera bounds to check if we have to flip our velocity
				_direction = (_direction == Vector2.left && transform.position.x > -_levelWidth + widthMargin)
							|| transform.position.x > _levelWidth - widthMargin
							? Vector2.left : Vector2.right;

				//move the stone left - right, make the velocity based on the score
				transform.Translate(_direction * (minimumSpeed + HunebedGameManager.Instance.Score) * Time.deltaTime);
				*/
				//Handle input
				if (Input.GetMouseButton(0))
				{
					Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
					rigidbody.freezeRotation = true;
					_isStatic = true;
				}
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			//Make the parts of the stone outside the bounds break off
			ContactPoint2D? xMin = null;
			ContactPoint2D? xMax = null;

			foreach(ContactPoint2D contact in collision.contacts)
			{
				xMin = !xMin.HasValue ? contact : xMin.Value.point.x > contact.point.x ? contact : xMin;
				xMax = !xMax.HasValue ? contact : xMax.Value.point.x < contact.point.x ? contact : xMax;
			}

			float length = xMax.Value.point.x - xMin.Value.point.x;

			float leftCutOff = -(-(_collider.bounds.size.x / 2) - transform.InverseTransformPoint(xMin.Value.point).x) / _pixelSizeWorldSpace;
			float rightCutOff = (_collider.bounds.size.x / 2 + transform.InverseTransformPoint(xMax.Value.point).x) / _pixelSizeWorldSpace;

			float percentCutoff = leftCutOff / (rightCutOff - _hunebedStoneSprite.width / 2 - leftCutOff);

			Rect bounds = new Rect(leftCutOff, 0f, rightCutOff - leftCutOff, _hunebedStoneSprite.height);
			GetComponent<SpriteRenderer>().sprite = Sprite.Create(_hunebedStoneSprite, bounds, new Vector2(0.5f - percentCutoff * 0.5f, 0.5f), 100f);

			Destroy(GetComponent<Rigidbody2D>());

			//TODO: Show the verticle stones (animation?)

			OnLand?.Invoke();
		}
	}
}
