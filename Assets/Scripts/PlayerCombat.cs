using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public Animator animator;

	public float attackRange = 0.2f;
	private bool canAttack;
	[SerializeField] AttackPoint attackPoint;

	public LayerMask enemyLayers;
	public LayerMask enemyBase;
	public int attackDamage = 20;

	public KnightMovement player;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		canAttack = attackPoint.CanAttack();
		
		if (Input.GetKeyDown(KeyCode.J))
		{
			//play an attack animation
			animator.SetTrigger("Attack");

		}
	}
	void Attack()
	{
		if (canAttack)
		{
			// Detect enemies in range of attack
			Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(player.attackPoint.position, attackRange, enemyLayers);

			//Damage them 
			foreach (Collider2D enemy in hitEnemies)
			{
				enemy.GetComponent<EnemyController>().TakeDame(attackDamage);
			}

			Collider2D[] EnemyBases = Physics2D.OverlapCircleAll(player.attackPoint.position, attackRange, enemyBase);
			foreach (Collider2D enemyBase in EnemyBases)
			{
				enemyBase.GetComponent<EnemyBase>().TakeDame(attackDamage);
			}
		}
		else
		{
			Debug.Log("Miss");
			return;
		}
		if (canAttack)
		{
			Debug.Log("can Attack");
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
