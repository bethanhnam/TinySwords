using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManage : MonoBehaviour
{
	public static ShopManage instance;
    public int coins;
    public Text coinText;
	

	private void Start()
	{
		if (instance == null)
			instance = this;
		
		coinText.text = coins.ToString();
	}
	public void AddCoin()
	{
		coins += 10;
		coinText.text = coins.ToString();
	}
	public bool RemoveCoin(int coin)
	{
		if (coins >= coin)
		{
			coins -= coin;
			coinText.text = coins.ToString();
			return true;
		}
		else
		{
			Debug.Log("khong du tien");
			return false;
		}
	}
}
