using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWalk : MonoBehaviour
{

	Rigidbody2D rb2D;
	float xMove = 0;
	public float speed = 5f;
	public GameObject visuals;
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
	public float rotTarget = 0;
	Hill hill = null;
	void Update()
	{
		grounded = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, Vector2.down, 0.9f, LayerMask.GetMask(new[] { "Ground" }));

		SlideUpdate();
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			BeginSlide();
		}
		if (!sliding)
		{
			visuals.transform.localPosition = new Vector2(0, (1 + Mathf.Sin(Time.time * 30)) * 0.005f * Mathf.Abs(xMove));
		}
		if (rb2D.isKinematic && hill)
		{
			HillUpdate();
		}

		CheckInput();
	}
	public bool rotating = false;
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
				rb2D.AddForce(Vector2.up * 300);
			}
		}
	}
	float hillPoint;
	void HillUpdate()
	{
		hill.speed *= 1.03f;
		hillPoint += hill.speed * Time.deltaTime;
		if (hillPoint > 1)
		{
			rb2D.velocity = (hill.GetPoint(1) - hill.GetPoint(0.9f)) * hill.speed * 20;//TODO: ADD DERIVATIVE OF FINAL POINT
			rb2D.isKinematic = false;
			hill = null;

		}
		else
		{
			Vector2 currentP = hill.GetPoint(hillPoint);
			Debug.Log(hillPoint);
			transform.position = currentP;
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
		if (!sliding && grounded)
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

	private void OnColliderEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Hill")
		{
			rb2D.isKinematic = true;
			hill = c.gameObject.GetComponent<Hill>();
			hillPoint = hill.PosToT(transform.position);
			hill.speed = Mathf.Abs(rb2D.velocity.x) * Time.fixedDeltaTime;
			rb2D.velocity = Vector2.zero;
		}
	}
}
