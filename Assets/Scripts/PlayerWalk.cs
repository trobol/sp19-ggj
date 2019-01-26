using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWalk : MonoBehaviour
{

	Rigidbody2D rb2D;
	float xMove = 0;
	public float speed = 5f;

	public bool sliding = false;
	public float rotateBy = 10;
	public Vector2 jumpForce = new Vector2(20, 150);
	public bool grounded = false;
	void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	void Start()
	{

	}

	public int direction = 1;
	float rotTime = 0;
	float rotTarget = 0;

	void Update()
	{
		RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, Vector2.down, Mathf.Infinity, LayerMask.GetMask(new[] { "Ground" }));
		grounded = hit.distance < 0.8f;
		SlideUpdate();
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			BeginSlide();
		}


		CheckInput();
	}
	bool rotating = false;
	void SlideUpdate()
	{
		if (sliding)
		{
			Quaternion target = Quaternion.Euler(0, direction > 0 ? 0 : 180, rotTarget);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateBy * Time.deltaTime);
			if (transform.rotation == target)
			{
				rotating = false;
				Debug.Log("Hit Target");
				if (rotTarget == 0)
				{
					sliding = false;
				}
			}
			if (Mathf.Abs(rb2D.velocity.x) < 0.05f && !rotating && rotTarget != 0)
			{
				rotTarget = 0;
				rb2D.AddForce(Vector2.up * 200);
			}
		}
	}
	private void FixedUpdate()
	{
		Move();
	}
	void CheckInput()
	{
		xMove = Input.GetAxis("Horizontal") * speed;
	}

	void Move()
	{
		if (!sliding)
		{
			rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
			if (xMove != 0)
			{
				direction = xMove > 0 ? 1 : -1;

				transform.rotation = Quaternion.Euler(0, direction > 0 ? 0 : 180, 0);
			}
		}
	}

	void BeginSlide()
	{
		if (!sliding)
		{
			sliding = true;
			if (grounded)
			{
				rb2D.AddForce(new Vector2(jumpForce.x * direction, jumpForce.y));
			}
			rotating = true;
			rotTarget = 90 * -1;
		}
	}
	private void OnCollisionEnter2D(Collision2D c)
	{

	}
}
