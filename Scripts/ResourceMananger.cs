using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceMananger : MonoBehaviour
{
	public static ResourceMananger Instance;

	private void Awake()
	{
		Instance = this;
	}

	public PopupText popupText;
}

