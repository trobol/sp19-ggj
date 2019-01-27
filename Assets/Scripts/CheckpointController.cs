using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckpointController : MonoBehaviour
{
    public GameManager gm;
    public GameObject player;
    public GameObject[] checkpoints;
    public GameObject currentCheckpoint;
    

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = checkpoints[0];
        checkpoints[0].GetComponent<Checkpoint>().isCurrentCP = true;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
    }

    void Respawn()
    {
        if(gm.isDead)
        {
            //StartCoroutine(gm.Fade());
            player.transform.position = currentCheckpoint.transform.position;
            gm.isDead = false;
        }
    }

    





}
