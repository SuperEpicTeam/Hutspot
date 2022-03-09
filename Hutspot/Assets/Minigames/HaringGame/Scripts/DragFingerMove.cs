using UnityEngine;

public class DragFingerMove : MonoBehaviour
{
	private Vector3 _touchPosition;
	private Rigidbody _rb;
	private Vector3 _direction;
	private float _moveSpeed = 10f;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		TouchInputMovement();
	}

	private void TouchInputMovement()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			_touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			_touchPosition.z = 0;
			_direction = (_touchPosition - transform.position);
			_rb.velocity = new Vector2(_direction.x, transform.position.y) * _moveSpeed;

			if (touch.phase == TouchPhase.Ended)
				_rb.velocity = Vector3.zero;
		}
	}
}

