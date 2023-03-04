using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : LivingEntity
{

	protected override void Start()
	{
		base.Start();
		GameEvents.OnPlayerHealthChanged?.Invoke(heath, startHealth);
	}
	public override void TakeDamege(float damage)
	{
		if (damage >= heath && !isDead) 
		{
			GameEvents.GameOver?.Invoke();
		}
		base.TakeDamege(damage);
		GameEvents.OnPlayerHealthChanged?.Invoke(heath,startHealth);
	}
}

