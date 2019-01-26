using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyser : MonoBehaviour
{

    public float gyserForce = 600f;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb2D;
        if(collision.tag == "Player")
        {
            ps.Play();
            rb2D = collision.GetComponent<Rigidbody2D>();
            //Debug.Log("Hit Gyser");
            rb2D.AddForce(new Vector2(rb2D.velocity.x, gyserForce));
            StartCoroutine(StopParticleSystem());
        }
    }


    IEnumerator StopParticleSystem()
    {
        yield return new WaitForSeconds(2.0f);
        ps.Stop();
    }
}
