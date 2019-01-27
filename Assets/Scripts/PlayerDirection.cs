using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    private Vector3 currentMouseLoc;
    private Vector3 adjustedPos;
    private Vector3 facing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentMouseLoc = Input.mousePosition;
        adjustedPos = Camera.main.ScreenToWorldPoint(currentMouseLoc);
        adjustedPos.z = 0;
        facing = Vector3.Normalize(adjustedPos - transform.position);
        if(facing.x < 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        }
    }
}
