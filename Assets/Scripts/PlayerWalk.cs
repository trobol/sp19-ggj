using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{

    public Rigidbody2D rb2D;
    public float xMove = 0;
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        rb2D.velocity = new Vector2(xMove, rb2D.velocity.y);
    }
}
