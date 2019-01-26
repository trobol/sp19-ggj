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
    public float jumpForce = 1;
    public bool grounded = false;
    void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }
    public Vector2 test;
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, Vector2.down, Mathf.Infinity, LayerMask.GetMask(new [] {"Ground"}));
        grounded = hit.distance < 0.8f;
        test = hit.normal;
        if(sliding) {
            if(grounded) {
                transform.rotation = Quaternion.FromToRotation(Vector3.right, Quaternion.Euler(0, 0, 90) * hit.normal);
            } else {
                if(false) {
                    transform.Rotate(new Vector3(0, 0, rotateBy));
                }

            }
        }
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            sliding = true;
            if(grounded) {
                rb2D.AddForce(Vector2.up * jumpForce);

            }
        }
        

        CheckInput();   
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
        if(!sliding) {
            rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D c) {
        
    }
}
