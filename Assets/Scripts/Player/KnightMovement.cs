using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	private SpriteRenderer rbSprite;
	private Animator rbAnimator;
	private Vector2 movement;
	private bool isFlip = false;

	public Transform attackPoint;

	public float moveSpeed = 5f; // Tốc độ di chuyển của Player
	private void Start()
	{
		rbSprite = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		rbAnimator = GetComponent<Animator>();
	}
	private void Update()
	{
		float horizontal = Input.GetAxis("Horizontal"); // Lấy giá trị đầu vào từ phím mũi tên trái/phải
		float vertical = Input.GetAxis("Vertical"); // Lấy giá trị đầu vào từ phím mũi tên lên/xuống
		movement = new Vector2(horizontal, vertical); // Tạo vector di chuyển
		ChangeAnimation();
		Flip();
	}
	private void FixedUpdate()
	{
		Move();
	}
	private void Move()
	{
		rb.velocity = movement * moveSpeed; // Cập nhật vận tốc của Player
	}
	private void Flip()
	{
		// Quay sprite renderer của nhân vật
		if (movement.x > 0)
		{
			rbSprite.flipX = false; // Quay sang phải
			isFlip = false;

		}
		else if (movement.x < 0)
		{
			rbSprite.flipX = true; // Quay sang trái
			isFlip = true;
		}
		changeAttackPoint();
	}
	private void ChangeAnimation()
	{
		if (movement.magnitude > 0)
		{
			rbAnimator.SetBool("isMoving", true); // Set Parameter
		}
		else
		{
			rbAnimator.SetBool("isMoving", false);
		}
	}
	void changeAttackPoint()
	{
		if (isFlip)
		{
			attackPoint.localPosition = new Vector3(-0.45f, 0, 0);
		}
		if(!isFlip)
		{
			attackPoint.localPosition = new Vector3(0.45f, 0, 0);
		}
	}
}