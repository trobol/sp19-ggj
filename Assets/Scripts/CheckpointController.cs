using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public GameObject[] checkpoints;
    public GameObject currentCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = checkpoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
