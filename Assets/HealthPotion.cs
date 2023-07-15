using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class HealthPotion : Potion
{
    public override void Use()
	{
		if (ShopManage.instance.RemoveCoin(price))
		{
			var playerHealth = player.GetComponent<PlayerHealth>();
			playerHealth.TakeHeal(10);
		}
		else
		{
			return;
		}
		
	}
}
