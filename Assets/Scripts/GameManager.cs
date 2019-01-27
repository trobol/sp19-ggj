using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isDead = false;
    public float cameraSize = 7;
    public bool onMenu;

    private void Awake()
    {
        if (Black != null && Black.enabled == false)
        {
            Black.enabled = true;
        }
        if (onMenu)
        {
            Black.color = new Color(0, 0, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
=======
        if(anim != null)
        {
            anim = GameObject.Find("Black").GetComponent<Animator>();
            StartCoroutine(Fade());
        }    
        if(music != null)
        {
            music.Play();
        }
>>>>>>> 34e923fb075ed5f1f35f998ae5727a2c0c0bd2c5
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
