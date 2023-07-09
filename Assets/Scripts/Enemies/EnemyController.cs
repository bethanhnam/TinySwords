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

	private Rigidbody2D rb;
	[SerializeField] private float moveSpeed = 1f; // Tốc độ di chuyển của enemy
	[SerializeField] public int enemyDmg = 20;

	public Animator animator;
	public float attackRange = 0.2f;
	public LayerMask PlayerMask;

	public Transform attackPoint;
	[SerializeField] float TimeBetweenAttack = 1f;
	[SerializeField] float attackTime = 0;

	AttackPoint attackPoint1;
	private bool canAttack;

	private void Start()
	{
		currentHealth = maxHealth;
		target = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		attackPoint1 = FindAnyObjectByType<AttackPoint>();
	}
	private void Update()
	{
		canAttack = attackPoint1.CanAttack();
		distanceWithTarget = Vector3.Distance(target.transform.position, this.transform.position);
		if (transform.position.x < target.transform.position.x)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		if (distanceWithTarget < 5f)
		{
			isDetected = true;
		}
		else
		{
			isDetected = false;
			animator.SetBool("isMoving", false);
		}
	}
	private void FixedUpdate()
	{
		if (isDetected)
		{
			if (distanceWithTarget <= 1.5f && canAttack)
			{
				animator.SetBool("isMoving", false);
				rb.velocity = Vector2.zero;
				attackTime += Time.deltaTime;
				if (attackTime > TimeBetweenAttack)
				{
					isAttacking = true;
					animator.SetTrigger("Attack");
				}
			}
			else 
			{
				MoveToTarget();
				animator.SetBool("isMoving", true);
			}
		}
	}
	private void MoveToTarget()
	{
		Vector3 nextPosition = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

		// Di chuyển enemy đến vị trí tiếp theo
		rb.MovePosition(nextPosition);
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
