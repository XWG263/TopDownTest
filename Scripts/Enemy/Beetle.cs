using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Beetle : LivingEntity
{
	[Header("AI")]
	public Transform target;
	public AIPath aiPath;

	[Header("Helee")]
	public LayerMask whatToHit;
	public float damage = 10;
	public float hitRate = 1.0f;
	private float _lastHit;

	Animator _animator;

	public void SetUp(Transform player,float upgrade)
	{
		target = player;
		aiPath.maxSpeed += upgrade;
		startHealth += upgrade * 10;
		damage += upgrade * 5;
		hitRate += upgrade * 2;
	}

	protected override void Start()
	{
		base.Start();
		_animator = GetComponent<Animator>();
		aiPath = GetComponent<AIPath>();
	}

	private void Update()
	{
		if (target == null)
		{
			aiPath.destination = transform.position;
			_animator.SetBool(AnimatorHash.IsMoving, false);
			return;
		}

		aiPath.destination = target.position;

		if (aiPath.reachedDestination)
		{
			_animator.SetBool(AnimatorHash.IsMoving, false);
			if (Time.time > _lastHit + 1 / hitRate)
			{
				Hit();
				_lastHit = Time.time;
			}
		}
		else
		{
			_animator.SetBool(AnimatorHash.IsMoving, true);
		}
	}

	void Hit()
	{
		Vector3 selfPosition = transform.position;
		//Direction to target
		Vector3 targetDirection = (target.position - selfPosition).normalized;
		RaycastHit2D hit2D = Physics2D.Raycast(selfPosition, targetDirection, aiPath.endReachedDistance + 2.0f, whatToHit);
		if (hit2D.collider != null)
		{
			IDemagable damageable = hit2D.transform.GetComponent<IDemagable>();
			damageable?.TakeDamege(damage);
		}


	}
}

