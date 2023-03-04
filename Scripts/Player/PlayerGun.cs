using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode
{
	Singel,
	Burst
}


public class PlayerGun : MonoBehaviour
{
	[SerializeField] PlayerController playerController;

	[SerializeField] Transform firePoint;

	[SerializeField] Transform shootTrail;

	[SerializeField] Transform muzzleFlash;

	[SerializeField] Transform hitParticle;

	//系统武器
	[Header("武器属性")]
	public LayerMask whatToHit;
	public FireMode fireMode = FireMode.Burst;
	public bool isReloading;
	public float nextShotTime;
	public int shotsNumber;
	public int clipSize;
	public int fireRate;
	public float reloadTime;
	public bool isTriggerRelease;

	public void OnTriggerHold(Vector2 mousePosition)
	{
		if (isReloading) return;

		if (shotsNumber >= clipSize)
		{
			StartCoroutine(ReloadCoroutine());
			return;
		}
		switch (fireMode)
		{
			case FireMode.Singel:
				{
					playerController.SetShootingState(false);
					if (!isTriggerRelease) return;
					Shoot(mousePosition);
					isTriggerRelease = false;
					break;
				}
			case FireMode.Burst:
				{
					if (Time.time < nextShotTime)
					{
						playerController.SetShootingState(false);
						return;
					}
					nextShotTime = Time.time + 1 / (float)fireRate;
					Shoot(mousePosition);
					break;
				}
			default:
				break;
		}
	}

	public void OnTriggerRelease()
	{
		Debug.Log("松开");
		isTriggerRelease = true;
		playerController.SetShootingState(false);
	}
	#region 装弹

	public void Reload()
	{
		StartCoroutine(ReloadCoroutine());
	}

	IEnumerator ReloadCoroutine()
	{
		Debug.Log("换弹");
		isReloading = true;
		yield return new WaitForSeconds(reloadTime);
		shotsNumber = 0;
		isReloading = false;
	}
#endregion
	#region 射击 射击效果
	void Shoot(Vector2 mousePosition)
	{
		playerController.SetShootingState(true);

		Vector2 shootDirection = GetShootdirection(mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(firePoint.position,shootDirection,100,whatToHit);
		if (hit.collider != null)
		{
			ShootEffect(hit);
		}
		else
		{
			ShootEffect(shootDirection);
		}
	}

	void ShootEffect(Vector2 shootDirection)
	{
		float trailAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
		Quaternion trailRotation = Quaternion.AngleAxis(trailAngle, Vector3.forward);
		Transform trail = Instantiate(shootTrail, firePoint.position, trailRotation);
		Destroy(trail.gameObject, 0.05f);

		MuzzleFlash();
	}
	void ShootEffect(RaycastHit2D hit)
	{
		Vector3 firePointPosition = firePoint.position;
		Transform trail = Instantiate(shootTrail, firePointPosition, Quaternion.identity);
		Destroy(trail.gameObject, 0.05f);
		LineRenderer lineRenderer = trail.GetComponent<LineRenderer>();
		Vector3 endPosition = new Vector3(hit.point.x, hit.point.y, firePointPosition.z);
		lineRenderer.useWorldSpace = true;
		lineRenderer.SetPosition(0, firePointPosition);
		lineRenderer.SetPosition(1, endPosition);

		//打击效果
		Transform sparks = Instantiate(hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
		Destroy(sparks.gameObject, 0.2f);

		//造成伤害
		IDemagable damageable = hit.transform.GetComponent<IDemagable>();

		if (damageable != null)
		{
			damageable?.TakeDamege(10);
			PopupText.Create(hit.point, Random.Range(1, 100), Random.Range(0, 100) < 30);
		}
		MuzzleFlash();
	}

	//枪口火光 
	void MuzzleFlash()
	{
		Transform flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
		flash.SetParent(firePoint);
		float randomSize = Random.Range(0.6f, 0.9f);
		flash.localScale = new Vector3(randomSize, randomSize, randomSize);
		Destroy(flash.gameObject, 0.05f);
	}
	#endregion
	Vector2 GetShootdirection(Vector2 mousePosition)
	{
		if (Vector2.Distance(mousePosition, firePoint.position) > 0.5f)
		{
			return mousePosition - (Vector2)firePoint.position;
		}
		return transform.up;
	}
}

