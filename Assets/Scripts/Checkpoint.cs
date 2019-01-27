using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointController checkpointController;
    public bool isCurrentCP = false;
    public Sprite inactive;
    public Sprite active;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        checkpointController = GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointController>();
        active = GetComponent<SpriteRenderer>().sprite;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Hit Checkpoint");
            for(int i = 0; i < checkpointController.checkpoints.Length; i++)
            {
                if(checkpointController.checkpoints[i] == this.gameObject)
                {
                    sound.Play();
                    isCurrentCP = true;
                    checkpointController.currentCheckpoint = this.gameObject;
                    GetComponent<SpriteRenderer>().sprite = inactive;
                }
                else
                {
                    checkpointController.checkpoints[i].GetComponent<Checkpoint>().isCurrentCP = false;
                }
            }
        }
    }
}
