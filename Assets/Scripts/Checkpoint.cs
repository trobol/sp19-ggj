using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointController checkpointController;
    public bool isCurrentCP = false;

    // Start is called before the first frame update
    void Start()
    {
        checkpointController = GameObject.FindGameObjectWithTag("GM").GetComponent<CheckpointController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit Checkpoint");
            for(int i = 0; i < checkpointController.checkpoints.Length; i++)
            {
                if(checkpointController.checkpoints[i] == this.gameObject)
                {
                    checkpointController.currentCheckpoint = this.gameObject;
                }
            }
        }
    }
}
