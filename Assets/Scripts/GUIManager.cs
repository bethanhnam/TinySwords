using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	private static GUIManager instance;
	public static GUIManager Instance => instance;
	//panel
	public GameObject pausePanel;
	public GameObject gameOverPanel;
	public GameObject WinPanel;
	public GameObject SelectionPanel;
	public GameObject MainMenuPanel;
	public GameObject ShopPanel;


	[SerializeField] private bool isOn = false;

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

	public void PausePanel()
	{
		isOn = !isOn;
		pausePanel.SetActive(isOn);
	}
	public void ClosePanel()
	{
		pausePanel.SetActive(false);
		isOn = false;
	}
	public void PlayGame()
	{
		MainMenuPanel.SetActive(false);
		SelectionPanel.SetActive(true);
	}
	public void OpenShopMenu()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 0;
		ShopPanel.SetActive(true);
	}
	public void CloseShopMenu()
	{
		Time.timeScale = 1;
		ShopPanel.SetActive(false);
		isOn = false;
	}
	public void GoHome()
	{
		SceneManager.LoadScene("Menu");
	}
}
