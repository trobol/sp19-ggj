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
    public Image Black;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = checkpoints[0];
        checkpoints[0].GetComponent<Checkpoint>().isCurrentCP = true;
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GameObject.Find("Black").GetComponent<Animator>();
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
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Black.color.a == 1);
        player.transform.position = currentCheckpoint.transform.position;
        gm.isDead = false;
        anim.SetBool("Fade", false);
        yield return new WaitUntil(() => Black.color.a == 1);
        
    }





}
