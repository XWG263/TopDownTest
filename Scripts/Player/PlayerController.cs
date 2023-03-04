using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
	public float moveSpeed = 12f;

	Vector2 _moveDirection;
	Rigidbody2D _rb;
	Animator _animator;
	Camera _mainCamera;
	PlayerGun _playerGun;
	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		_animator = GetComponent<Animator>();
		_mainCamera = Camera.main;
		_playerGun = GetComponentInChildren<PlayerGun>();
		if (_mainCamera == null)
		{
			Debug.LogError("can't find main camera");
		}
	}

	private void Update()
	{
		//移动
		_moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		_moveDirection *= moveSpeed;

		_animator.SetFloat(AnimatorHash.MoveSpeed, Mathf.Abs(_moveDirection.x) * Mathf.Abs(_moveDirection.y));

		//转向射击
		Vector3 direction = Input.mousePosition - _mainCamera.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

		//输入
		if (Input.GetMouseButton(0))
		{
			//扣动扳机
			_playerGun.OnTriggerHold(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
		if (Input.GetMouseButtonUp(0))
		{
			_playerGun.OnTriggerRelease();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			_playerGun.Reload();
		}
	}

	private void FixedUpdate()
	{
		_rb.AddForce(_moveDirection, ForceMode2D.Impulse);
	}

	public void SetShootingState(bool isShooting)
	{
		_animator.SetBool(AnimatorHash.IsShooting, isShooting);
	}
}

