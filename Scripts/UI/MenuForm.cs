using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuForm : MonoBehaviour
{
	[SerializeField] ToggleGroup toggleGroup;

	Toggle currentSelection => GetFirstActiveToggle(toggleGroup);
	Toggle onToggle;

	private void Start()
	{
		var toggles = toggleGroup.GetComponentsInChildren<Toggle>();
		foreach (var toggle in toggles)
		{
			toggle.onValueChanged.AddListener(_ => OnToggleValueChanged(toggle));
		}

		currentSelection.onValueChanged?.Invoke(true);
	}

	private void OnToggleValueChanged(Toggle toggle)
	{
		if (currentSelection == onToggle)
		{
			switch (toggle.name)
			{
				case "GameStart":
					SceneManager.LoadScene("Game");
					break;
				case "Settings":
					break;
				case "Sponsor":
					break;
				case "Quit":
					Application.Quit();
#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
#endif
					break;
				default:
					break;
			}
			return;
		}

		if (toggle.isOn)
		{
			onToggle = toggle;
			onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.yellow;
		}
		else
		{
			onToggle.transform.Find("Label").GetComponent<TMP_Text>().color = Color.white;
		}
	}

	Toggle GetFirstActiveToggle(ToggleGroup t)
	{
		foreach (var item in t.ActiveToggles())
		{
			if (item.IsActive())
			{
				return item;
			}
		}
		return null;
	}
}

