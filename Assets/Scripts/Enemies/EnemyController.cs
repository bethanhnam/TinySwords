using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyController : MonoBehaviour
{
	private GameObject target;
	[SerializeField] private float distanceWithTarget;
	private bool isDetected = false;
	private bool isAttacking = false;
	

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
		target = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	private void Update()
	{
		distanceWithTarget = Vector3.Distance(target.transform.position, this.transform.position);
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
		if (isDetected && !isAttacking)
		{
			if (distanceWithTarget > 1.5f)
			{
				MoveToTarget();
			}
			else
			{
					attackTime += Time.deltaTime;
					if (attackTime > TimeBetweenAttack)
					{
						isAttacking = true;
						animator.SetTrigger("Attack");

					}

			}
		}
	}
	private void MoveToTarget()
	{
		// Tính toán vị trí tiếp theo của enemy
		Vector3 nextPosition = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x - 1f, target.transform.position.y, 0), moveSpeed * Time.deltaTime);

		// Di chuyển enemy đến vị trí tiếp theo
		rb.MovePosition(nextPosition);
	}
	public void TakeDame(int dmg)
	{
		GameManager.Instance._enemyHealth.DmgUnit(dmg);
		Debug.Log(GameManager.Instance._enemyHealth.Health);
		if (GameManager.Instance._enemyHealth.Health <= 0)
		{
			Die();
		}
	}
	public void TakeHeal(int helling)
	{
		GameManager.Instance._enemyHealth.HealthUnit(helling);
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
