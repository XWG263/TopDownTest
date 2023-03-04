
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverForm : MonoBehaviour
{
	[SerializeField] private GameObject masks;
	[SerializeField] private Button menuButton;
	[SerializeField] private Button restartButton;
	[SerializeField] private Button quitButton;

	private void Awake()
	{
		masks.SetActive(false);

		menuButton.onClick.AddListener(OnMenuButtonClick);
		restartButton.onClick.AddListener(OnRestartButtonClick);
		quitButton.onClick.AddListener(OnQuitButtonClick);
	}

	private void OnEnable()
	{
		GameEvents.GameOver += GameOver;
	}

	private void OnDisable()
	{
		GameEvents.GameOver -= GameOver;
	}

	void OnMenuButtonClick()
	{
		SceneManager.LoadScene("Menu");
	}
	void OnRestartButtonClick()
	{
		SceneManager.LoadScene("Game");
	}
	void OnQuitButtonClick()
	{
		Application.Quit();
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}

	void GameOver()
	{
		masks.SetActive(true);
	}
}

