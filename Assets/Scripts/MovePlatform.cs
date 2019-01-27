using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Vector2 point1;
    public Vector2 point2;

    public bool goingTo = false;

    public float speed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (transform.position.x == point2.x && transform.position.y == point2.y)
            goingTo = true;
        else
            goingTo = false;

        if (goingTo)
        {
            transform.position = Vector2.MoveTowards(transform.position, point1, step);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, point2, step);
        }
    }
}
