using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyser : MonoBehaviour
{
    public GameManager gm;
    public float gyserForce = 600f;
    public ParticleSystem ps;
    public Camera cam;
    public float zoomOut = 10f;
    public float zoomTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        cam = Camera.main;
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
            StartCoroutine(ZoomOut(cam.orthographicSize, zoomOut, zoomTime));
            rb2D = collision.GetComponent<Rigidbody2D>();
            //Debug.Log("Hit Gyser");
            rb2D.AddForce(new Vector2(rb2D.velocity.x, gyserForce));
            StartCoroutine(ResetCameraSize(zoomOut, gm.cameraSize, zoomTime/2));
            StartCoroutine(StopParticleSystem());
        }
    }

    IEnumerator ZoomOut(float startSize, float endSize, float time)
    {
        float elapsedTime = 0;
        while(elapsedTime < time)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, endSize, (elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ResetCameraSize(float startSize, float endSize, float time)
    {
        yield return new WaitForSeconds(2.0f);
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, endSize, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator StopParticleSystem()
    {
        yield return new WaitForSeconds(2.0f);
        ps.Stop();
    }
}
