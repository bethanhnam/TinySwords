using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
	[SerializeField] public static bool canAttack = false;
	public bool CanAttack()
	{
		return canAttack;
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Enemy") || collision.CompareTag("EnemyBase"))
		{
			canAttack = true;
		}
		else
		{
			canAttack = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player") || collision.CompareTag("Enemy") || collision.CompareTag("EnemyBase"))
		{
			canAttack = false;
		}
	}
}
