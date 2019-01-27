using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image Black;
    public Animator anim;
    public AudioSource music;
    public bool isDead = false;
    public float cameraSize = 7;

    // Start is called before the first frame update
    void Start()
    {
        if(Black.enabled == false)
        {
            Black.enabled = true;
        }
        anim = GameObject.Find("Black").GetComponent<Animator>();
        StartCoroutine(Fade());
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Fade()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => Black.color.a == 1);
        anim.SetBool("Fade", false);
        yield return new WaitUntil(() => Black.color.a == 1);

    }
}
