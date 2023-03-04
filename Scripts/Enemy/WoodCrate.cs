using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCrate : LivingEntity
{
	public override void TakeDamege(float damage)
	{
		if (damage >=heath && !isDead)
		{
			OnDeath += () => { Debug.Log("wood dead"); };
		}
		//死亡特效
		//死亡音效
		base.TakeDamege(damage);
	}

}

