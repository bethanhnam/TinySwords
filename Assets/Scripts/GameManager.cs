using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance => instance;
	public enum gameState { GameStarted ,GameOver,WinGame};
	gameState state;
	[SerializeField] private GameObject player;
	[SerializeField] private PlayerHealth playerHealth;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}
	private void Start()
	{
		state = gameState.GameStarted;
		player = GameObject.Find("Knight");
		playerHealth = player.GetComponentInChildren<PlayerHealth>();
	}
	private void Update()
	{
		if(playerHealth.currentHealth <= 0)
		{
			state = gameState.GameOver;
			GUIManager.Instance.gameOverPanel.SetActive(true);
		}
		if(playerHealth.currentHealth > 0)
		{
			if (state == gameState.WinGame)
			{
				GUIManager.Instance.WinPanel.SetActive(true);
			}
		}
	}
	public void Replay()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
		GUIManager.Instance.gameOverPanel.SetActive(false);
		GUIManager.Instance.WinPanel.SetActive(false);
	}
}
