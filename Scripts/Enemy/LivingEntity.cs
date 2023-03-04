using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour, IDemagable
{
	public float startHealth;
	protected float heath { get; private set; }
	protected bool isDead;

	public ParticleSystem deathParticle;

	public event Action OnDeath;

	protected virtual  void Start()
	{
		heath = startHealth;
	}

	public virtual void TakeDamege(float damage)
	{
		heath -= damage;
		if (heath > 0 || isDead) return;

		isDead = true;
		OnDeath?.Invoke();

		Destroy(gameObject);

		//死亡特效
		if (deathParticle == null)
		{
			Debug.LogError("No deathParticle");
			return;
		}
		ParticleSystem ps = Instantiate(deathParticle, transform.position, Quaternion.identity);
		Destroy(ps.gameObject,ps.main.duration);
	}
}

