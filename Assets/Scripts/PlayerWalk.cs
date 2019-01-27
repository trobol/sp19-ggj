using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: CANNOT SLIDE AND SWIM AT THE SAME TIME
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWalk : MonoBehaviour
{

	Rigidbody2D rb2D;
	Vector2 move;
	public float speed = 5f;
	public GameObject visuals;
	public bool sliding = false;
	public float rotateBy = 10;
	Vector2 jumpForce = new Vector2(200, 200);
	public bool grounded = false;
	void Awake()
	{
		rb2D = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		
	}
	bool swimming = false;
	public int direction = 1;
	float rotTime = 0;
	public float rotTarget = 0;
	Hill hill = null;
	void Update()
	{
		//new
		if (!swimming)
		{
		//end
			SlideUpdate();

		}
		if(!sliding) {
			direction = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x < 0 ? -1 : 1;
			transform.rotation = Quaternion.Euler(0, direction > 0 ? 0 : 180, 0);
			
			//visuals.transform.localRotation = Quaternion.AxisAngle(Vector3.forward, Mathf.Abs(Mathf.Sin(Time.time * 30)) * 90 * Mathf.Abs(move.x));
			//visuals.transform.localPosition = new Vector2(0, (1 + Mathf.Sin(Time.time * 30)) * 0.005f * Mathf.Abs(move.x));
		}
		CheckInput();
	}
	public bool rotating = false;
	void SlideUpdate()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			BeginSlide();
		}
		if (sliding)
		{

			Quaternion target = Quaternion.Euler(0, direction > 0 ? 0 : 180, rotTarget);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateBy * Time.deltaTime);

			if (transform.rotation == target)
			{
				rotating = false;
				if (rotTarget == 0)
				{
					rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
					sliding = false;
				}
			}
			if (Mathf.Abs(rb2D.velocity.x) < 0.1f && !rotating && rotTarget != 0 )
			{
				rotTarget = 0;
				rb2D.AddForce(Vector2.up * 400);
			}
		}
	}


	private void FixedUpdate()
	{
		RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, Vector2.down, 0.9f, LayerMask.GetMask(new[] { "Ground" }));
		grounded = hit;
		if (swimming)
		{
			rb2D.velocity = move;
		}
		else if (!sliding && grounded)
		{
			rb2D.velocity = new Vector2(move.x, rb2D.velocity.y);
		}

	}
	void CheckInput()
	{
		//new
		move.x = Input.GetAxis("Horizontal") * speed;
		move.y = Input.GetAxis("Vertical") * speed;
		//end
	}
	void BeginSlide()
	{
		if (!sliding)
		{
			sliding = true;
			
			rb2D.constraints = RigidbodyConstraints2D.None;
			if (grounded)
			{
				rb2D.AddForce(new Vector2(jumpForce.x * direction, jumpForce.y));
			}
			rotating = true;
			rotTarget = 90 * -1;
		}
	}

	private void OnTriggerStay2D(Collider2D c)
	{
		if (c.tag == "Water")
		{
			swimming = true;
		}

	}

	private void OnTriggerExit2D(Collider2D c)
	{
		if (c.tag == "Water")
		{
			swimming = false;
		}
	}
}
