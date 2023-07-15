using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Antidote : Potion
{
	public override void Use()
	{
		if (ShopManage.instance.RemoveCoin(price))
		{
			var playerCombat = player.GetComponent<PlayerCombat>();
			playerCombat.AddDamage(5);
		}
		else
		{
			return;
		}
	}
}
