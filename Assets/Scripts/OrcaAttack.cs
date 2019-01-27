using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaAttack : MonoBehaviour
{
    public GameObject player;

    public float orcaSpeed;

    private float timeToSlow = 0;

    public float slowRate = 0;

    public float agroRange = 0;

    public float knockBack = 0;

    public float rows = 0;

    public float cols = 0;

    GameObject water;

    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) < agroRange) moveTowards(player);
    }

    void moveTowards(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        dir.z = 0;
        if (Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) < 4 && Time.time > timeToSlow)
        {
            timeToSlow += Time.time + slowRate;
            orcaSpeed -= 0.01f;
        }
        transform.right = target.transform.position - transform.position;
        transform.position += dir * Time.deltaTime * orcaSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(6);
            other.GetComponent<Rigidbody2D>().AddForce(-transform.right * knockBack);
        }
    }
}
