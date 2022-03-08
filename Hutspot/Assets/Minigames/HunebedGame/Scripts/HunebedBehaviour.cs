using UnityEngine;

namespace Hutspot.Minigames.HunebedGame
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class HunebedBehaviour : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D _leftVerticleStone;
		[SerializeField] private BoxCollider2D _rightVerticleStone;
		[SerializeField] private Texture2D _hunebedStoneSprite;

		private float _levelWidth;
		private Vector2 _direction;
		private bool _isStatic;

		private BoxCollider2D _collider;
		private Rigidbody2D _rigidBody;

		public delegate void OnLandEvent();

		public event OnLandEvent OnLand;
		public event OnLandEvent OnDie;

		private void Awake()
		{
			_collider = transform.GetComponent<BoxCollider2D>();

			_leftVerticleStone.gameObject.SetActive(false);
			_rightVerticleStone.gameObject.SetActive(false);

			_levelWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
		}

		private void Update()
		{
			const int minimumSpeed = 1;
			const float moveSpeedMultiplier = 0.1f;

			if(!_isStatic)
			{
				//Get the camera bounds to check if we have to flip our velocity
				_direction = (_direction == Vector2.left && transform.position.x > -_levelWidth + _collider.bounds.extents.x)
							|| transform.position.x > _levelWidth - _collider.bounds.extents.x
							? Vector2.left : Vector2.right;

				//move the stone left - right, make the velocity based on the score
				transform.Translate(_direction * (minimumSpeed + HunebedGameManager.Instance.Score * moveSpeedMultiplier) * Time.deltaTime);
				
				//Handle input
				if (Input.GetMouseButton(0))
				{
					_rigidBody = gameObject.AddComponent<Rigidbody2D>();
					_rigidBody.freezeRotation = true;
					_isStatic = true;
				}
			} 
			else if (_rigidBody != null)
			{
				if (transform.position.y < HunebedGameManager.Instance.PreviousY)
				{
					OnDie?.Invoke();
					Destroy(this.gameObject);
				}
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (_rigidBody != null)
			{
				BreakStone();
				ShowVerticleStones();

				Destroy(_rigidBody);
				OnLand?.Invoke();
			}
		}

		private void BreakStone()
		{
			//Make the parts of the stone outside the bounds break off
			Vector2 xMin = Vector2.positiveInfinity;
			Vector2 xMax = Vector2.negativeInfinity;

			Collider2D[] collidersInOverlap = Physics2D.OverlapBoxAll(transform.position, _collider.size * 1.1f, 0f);

			foreach (Collider2D collider in collidersInOverlap)
			{
				if(collider.tag.CompareTo("Player") != 0)
				{
					Vector2 min = collider.transform.position - collider.bounds.extents;
					Vector2 max = collider.transform.position + collider.bounds.extents;

					xMin = min.x < xMin.x ? min : xMin;
					xMax = max.x > xMax.x ? max : xMax;
				}
			}

			Vector2 boundsMin = transform.position - _collider.bounds.extents;
			Vector2 boundsMax = transform.position + _collider.bounds.extents;

			xMin = xMin.x < boundsMin.x ? boundsMin : xMin;
			xMax = xMax.x > boundsMax.x ? boundsMax : xMax;

			float length = Mathf.Abs(xMax.x - xMin.x);

			transform.SetPositionAndRotation(new Vector3(xMin.x + length / 2, transform.position.y), Quaternion.identity);
			_collider.size = new Vector2(length * (1f / transform.localScale.x), _collider.size.y);
		}

		private void ShowVerticleStones()
		{
			float xLeft = transform.position.x - _collider.bounds.extents.x;
			float xRight = transform.position.x + _collider.bounds.extents.x;

			float stoneHeight = _leftVerticleStone.size.y;
			float yPos = transform.position.y + stoneHeight / 2 + _collider.size.y / 2;

			xLeft += _leftVerticleStone.bounds.extents.x;
			xRight -= _rightVerticleStone.bounds.extents.x;

			_leftVerticleStone.transform.position = new Vector3(xLeft, yPos);
			_rightVerticleStone.transform.position = new Vector3(xRight, yPos);

			_leftVerticleStone.gameObject.SetActive(true);
			_rightVerticleStone.gameObject.SetActive(true);
		}
	}
}
