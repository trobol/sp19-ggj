using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Transform startLoc;
    public Transform endLoc;
    public bool canMove = false;
    public float speed = 1.0f;
    public bool atStart = false;
    public bool atEnd = false;


    // Start is called before the first frame update
    void Start()
    {
        if(canMove)
        {
            transform.position = startLoc.position;
            atStart = true;
        }     
    }

    // Update is called once per frame
    void Update()
    {
        moveToEnd();
    }

    void moveToEnd()
    {
        if(atStart)
        {
            transform.position = Vector3.MoveTowards(transform.position, endLoc.position, speed * Time.deltaTime);
        }
        if (atEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, startLoc.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "StartLoc")
        {
            atEnd = false;
            atStart = true;
        }
        if (collision.gameObject.tag == "EndLoc")
        {
            atStart = false;
            atEnd = true;
        }
    }

}
