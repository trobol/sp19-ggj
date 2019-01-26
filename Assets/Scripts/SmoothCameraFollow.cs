using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public GameManager gm;
    public Transform target;
    public Vector3 offset;
    public Camera cam;
    public float smoothTime;
    float size;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        size = gm.cameraSize;
        cam.orthographicSize = size;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
        transform.position = smoothPosition;
    }
}
