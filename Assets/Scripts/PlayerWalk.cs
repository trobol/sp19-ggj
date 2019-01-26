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
    
    int rotDirection = -1;
    float rotTarget = 0;
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.5f), 0, Vector2.down, Mathf.Infinity, LayerMask.GetMask(new [] {"Ground"}));
        grounded = hit.distance < 0.8f;
        if(sliding) {
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -90), rotateBy * Time.deltaTime);
            if(transform.rotation.eulerAngles.z == rotTarget) {
                Debug.Log("Hit Target");
            }
            if(rb2D.velocity.x == 0) {
                
            }
            Debug.Log(transform.rotation.eulerAngles.z);
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
            rotTarget = 90 * rotDirection;
        }
    }
    private void OnCollisionEnter2D(Collision2D c) {
        
    }
}
