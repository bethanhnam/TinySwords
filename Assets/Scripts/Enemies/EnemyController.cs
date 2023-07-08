using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
	private GameObject target;
	[SerializeField] private float distanceWithTarget;
	private bool isDetected = false;
	private bool isAttacking = false;

	[SerializeField] private int currentHealth;
	[SerializeField] private int maxHealth;

	private Vector2 movement;
	public Vector3 dir;
	private Rigidbody2D rb;
	[SerializeField] private float moveSpeed = 1f; // Tốc độ di chuyển của enemy
	[SerializeField] public int enemyDmg = 20;

	public Animator animator;
	public float attackRange = 0.2f;
	public LayerMask PlayerMask;

	public Transform attackPoint;
	[SerializeField] float TimeBetweenAttack = 1f;
	[SerializeField] float attackTime = 0;
	private float attackRangeY = 0.1f;

	private void Start()
	{
		currentHealth = maxHealth;
		target = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		distanceWithTarget = Vector3.Distance(target.transform.position, this.transform.position);
		dir = target.transform.position - this.transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		dir.Normalize();
		movement = dir;
		if (distanceWithTarget < 5f)
		{
			isDetected = true;
		}
		else
		{
			isDetected = false;
		}
	}
	private void FixedUpdate()
	{
		if (isDetected)
		{
			if (distanceWithTarget > 1.5f && !isAttacking)
			{
				MoveToTarget(movement);
				animator.SetBool("isMoving", true);
			}
			else
			{
				rb.velocity = Vector2.zero;
				animator.SetBool("isMoving", false);
				attackTime += Time.deltaTime;
				if (attackTime > TimeBetweenAttack)
				{
					isAttacking = true;
					animator.SetTrigger("Attack");

				}

			}
		}
	}
	private void MoveToTarget(Vector2 dir)
	{
		rb.MovePosition((Vector2)transform.position + (dir * moveSpeed * Time.deltaTime)); 
	}
	public void TakeDame(int dmg)
	{
		currentHealth -= dmg;
		Debug.Log(currentHealth + transform.name);
		if (currentHealth <= 0)
		{
			Die();
		}
	}
	void Die()
	{
		Destroy(gameObject);
	}
	public void Attack()
	{
		//play an attack animation
		Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, PlayerMask);
		//Damage them 
		foreach (Collider2D player in hitPlayer)
		{
			player.GetComponent<PlayerHealth>().TakeDame(enemyDmg);
		}
		attackTime = 0;
		isAttacking = false;
	}
	private void OnDrawGizmosSelected()
	{
		if (attackPoint == null)
		{
			return;
		}
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}


}
