using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct MapBound
{
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public class CameraFllow : MonoBehaviour
{
	public Transform target;
	public float smoothTime = .4f;
	public MapBound mapBound;

	float _offsetZ;
	Vector3 _currenVelocity;

	private void Start()
	{
		if (target == null)
		{
			Debug.LogError("can't find player");
			return;
		}

		_offsetZ = (transform.position - target.position).z;
	}

	private void FixedUpdate()
	{
		if (target == null) return;

		Vector3 targetPostion = target.position + Vector3.forward * _offsetZ;
		Vector3 newPostion = Vector3.SmoothDamp(transform.position, targetPostion, ref _currenVelocity, smoothTime);

		newPostion.x = Mathf.Clamp(newPostion.x,mapBound.xMin,mapBound.xMax);
		newPostion.y = Mathf.Clamp(newPostion.y, mapBound.yMin, mapBound.yMax);

		transform.position = newPostion;
	}
}

