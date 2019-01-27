using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealAttacks : MonoBehaviour
{
    private float timeToNextAttack = 0; //How much longer until player can attack again

    [HideInInspector]
    public GameObject bulletSpawn;

    public GameObject projectile;

    public GameObject direction;

    public float attackRate = 0;

    public float bulletSpeed;

    public float bulletDespawn;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawn = GameObject.Find("BulletSpawn");
        //direction = GameObject.Find("");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToNextAttack) fire();
    }

    void fire()
    {
        timeToNextAttack = Time.time + attackRate;
        GameObject bullet = Instantiate(projectile, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bullet.transform.right = direction.transform.position - bulletSpawn.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * bullet.transform.right;
        Destroy(bullet, bulletDespawn);
    }
}
