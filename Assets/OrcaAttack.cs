using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaAttack : MonoBehaviour
{
    public GameObject player;

    public float orcaSpeed;

    private float timeToSlow = 0;

    public float slowRate = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTowards(player);
    }

    void moveTowards(GameObject target)
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        dir.z = 0;
        if(Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) < 4 && Time.time > timeToSlow)
        {
            timeToSlow += Time.time + slowRate;
            orcaSpeed -= 0.01f;
        }
        transform.right = target.transform.position - transform.position;
        transform.position += dir * Time.deltaTime * orcaSpeed;
    }
}
