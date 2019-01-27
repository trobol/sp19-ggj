using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dealDamage : MonoBehaviour
{
    public int playerDamage;

    public int[] enemyDamage = new int[5];

    public int currProjectile = 0;

    void Start()
    {
        if(gameObject.tag == "Snowball")
        {
            currProjectile = 0;
        }
        else if (gameObject.tag == "Icicle")
        {
            currProjectile = 1;
        }
        else if (gameObject.tag == "SnowSpray")
        {
            currProjectile = 2;
        }
        else if (gameObject.tag == "Deadfish")
        {
            currProjectile = 3;
        }
        else if (gameObject.tag == "ShotGun")
        {
            currProjectile = 4;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(playerDamage);
            Destroy(gameObject);
        }
        else if(collision.tag == "Enemy")
        {
            Destroy(gameObject);
            collision.GetComponent<EnemyHealth>().hit = true;
            collision.GetComponent<EnemyHealth>().damageAmount = enemyDamage[currProjectile];
        }
    }
}
