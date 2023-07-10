using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : PlayerHealth
{
	private SpriteRenderer enemyBaseSprite;
	[SerializeField]private Sprite enemyBaseDestroyedSprite;
	[SerializeField] private GameObject enemyBaseCanvas;

	private void Start()
	{
		enemyBaseSprite = GetComponent<SpriteRenderer>();
		Debug.Log(PlayerPrefs.GetInt("ReachedIndex"));
		Debug.Log(PlayerPrefs.GetInt("UnlockedLevel"));
	}
	private void Update()
	{
		if(base.currentHealth < 0)
		{
			enemyBaseSprite.sprite = enemyBaseDestroyedSprite;
			base._healthBar.enabled = false;
			enemyBaseCanvas.SetActive(false);
			UnlockNewLevel();
		}
	}
	void UnlockNewLevel()
	{
		if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
		{
			PlayerPrefs.SetInt("ReachedIndex",SceneManager.GetActiveScene().buildIndex + 1);
			PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
			PlayerPrefs.Save();
			
		}
	}
}
