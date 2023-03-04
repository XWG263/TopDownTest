
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainForm : MonoBehaviour
{
	[SerializeField] private Image healthBar;
	[SerializeField] private TMP_Text healthText;

	private void OnEnable()
	{
		GameEvents.OnPlayerHealthChanged += OnPlayerHealthChanged;
	}

	void OnPlayerHealthChanged(float currentHealth,float startHealth)
	{
		healthBar.fillAmount = currentHealth / startHealth;
		healthText.SetText($"{currentHealth}/{startHealth}");
	}
	private void OnDisable()
	{
		GameEvents.OnPlayerHealthChanged -= OnPlayerHealthChanged;
	}
}

