using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public Animator animator;

	public float attackRange = 0.2f;


	public LayerMask enemyLayers;
	public int attackDamage = 20;

	public KnightMovement player;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			//play an attack animation
			animator.SetTrigger("Attack");

		}
	}
	void Attack()
	{
		// Detect enemies in range of attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackPoint.position, attackRange, enemyLayers);

		//Damage them 
		foreach (Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<EnemyController>().TakeDame(attackDamage);
		}
	}
	private void OnDrawGizmosSelected()
	{
		if (player.attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(player.attackPoint.position, attackRange);
	}

}
