using UnityEngine;

namespace Hutspot.Minigames.HunebedGame
{
	public class HunebedBehaviour : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D _leftVerticleStone;
		[SerializeField] private BoxCollider2D _rightVerticleStone;
		[SerializeField] private Texture2D _hunebedStoneSprite;

		private float _levelWidth;
		private Vector2 _direction;
		private bool _isStatic;
		private float _pixelSizeWorldSpace;

		private BoxCollider2D _collider;
		private SpriteRenderer _renderer;
		private Rigidbody2D _rigidBody;

		public delegate void OnLandEvent();

		public event OnLandEvent OnLand;
		public event OnLandEvent OnDie;

		private void Awake()
		{
			_collider = transform.GetComponent<BoxCollider2D>();
			_renderer = transform.GetComponent<SpriteRenderer>();

			_leftVerticleStone.gameObject.SetActive(false);
			_rightVerticleStone.gameObject.SetActive(false);
		}

		private void Start()
		{
			_levelWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
			_pixelSizeWorldSpace = _collider.bounds.size.x / _hunebedStoneSprite.width;
		}

		private void Update()
		{
			const int minimumSpeed = 1;
			const float widthMargin = 5f;

			if(!_isStatic)
			{
				//Get the camera bounds to check if we have to flip our velocity
				_direction = (_direction == Vector2.left && transform.position.x > -_levelWidth + widthMargin)
							|| transform.position.x > _levelWidth - widthMargin
							? Vector2.left : Vector2.right;

				//move the stone left - right, make the velocity based on the score
				transform.Translate(_direction * (minimumSpeed + HunebedGameManager.Instance.Score) * Time.deltaTime);
				
				//Handle input
				if (Input.GetMouseButton(0))
				{
					_rigidBody = gameObject.AddComponent<Rigidbody2D>();
					_rigidBody.freezeRotation = true;
					_isStatic = true;
				}
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (_rigidBody != null)
			{
				BreakStone(collision);
				ShowVerticleStones();

				Destroy(_rigidBody);
				OnLand?.Invoke();
			}
		}

		private void BreakStone(Collision2D collision)
		{
			//Make the parts of the stone outside the bounds break off
			ContactPoint2D? xMin = null;
			ContactPoint2D? xMax = null;

			Collider2D overlapCollider = Physics2D.OverlapBox(transform.position, _collider.size * 1.1f, 0f);
			Collider2D[] collidersInOverlap = new Collider2D[2];

			print(overlapCollider.OverlapCollider(new ContactFilter2D().NoFilter(), collidersInOverlap));
			print(collidersInOverlap);

			foreach (Collider2D collider in collidersInOverlap)
			{
				foreach (ContactPoint2D contact in collision.contacts)
				{
					xMin = !xMin.HasValue ? contact : xMin.Value.point.x > contact.point.x ? contact : xMin;
					xMax = !xMax.HasValue ? contact : xMax.Value.point.x < contact.point.x ? contact : xMax;
				}
			}

			float length = xMax.Value.point.x - xMin.Value.point.x;

			float leftCutOff = -(-(_collider.bounds.size.x / 2) - transform.InverseTransformPoint(xMin.Value.point).x) / _pixelSizeWorldSpace;
			float rightCutOff = (_collider.bounds.size.x / 2 + transform.InverseTransformPoint(xMax.Value.point).x) / _pixelSizeWorldSpace - leftCutOff;

			transform.position = new Vector3(xMin.Value.point.x + length / 2, transform.position.y);

			Rect bounds = new Rect(leftCutOff, 0f, rightCutOff, _hunebedStoneSprite.height);
			_renderer.sprite = Sprite.Create(_hunebedStoneSprite, bounds, new Vector2(0.5f, 0.5f), 100f);

			_collider.size = new Vector2(length, _collider.size.y);
		}

		private void ShowVerticleStones()
		{
			float xLeft = transform.position.x - _collider.size.x / 2;
			float xRight = transform.position.x + _collider.size.x / 2;

			float stoneHeight = _leftVerticleStone.size.y;
			float yPos = transform.position.y + stoneHeight / 2 + _collider.size.y / 2;

			xLeft += _leftVerticleStone.size.x / 2;
			xRight -= _rightVerticleStone.size.x / 2;

			_leftVerticleStone.transform.position = new Vector3(xLeft, yPos);
			_rightVerticleStone.transform.position = new Vector3(xRight, yPos);

			_leftVerticleStone.gameObject.SetActive(true);
			_rightVerticleStone.gameObject.SetActive(true);
		}
	}
}
