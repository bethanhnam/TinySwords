using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
	private static GUIManager instance;
	public static GUIManager Instance => instance;
	//panel
	public GameObject pausePanel;
	public GameObject gameOverPanel;
	public GameObject WinPanel;


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
}