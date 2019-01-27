using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dealDamage : MonoBehaviour
{
    public int playerDamage;

    private int[] enemyDamage = new int[5];

    void Start()
    { 
       
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
            collision.GetComponent<EnemyHealth>().hit = true;
            if (gameObject.tag == "Snowball")
            {
                collision.GetComponent<EnemyHealth>().damageAmount = enemyDamage[0];
                Destroy(gameObject);
            }
            else if (gameObject.tag == "Icicle")
            {
                collision.GetComponent<EnemyHealth>().damageAmount = enemyDamage[1];
            }
            else if (gameObject.tag == "SnowSpray")
            {
                collision.GetComponent<EnemyHealth>().damageAmount = enemyDamage[2];
                Destroy(gameObject);
            }
            else if (gameObject.tag == "Deadfish")
            {
                collision.GetComponent<EnemyHealth>().damageAmount = enemyDamage[3];
                Destroy(gameObject);
            }
            else if (gameObject.tag == "ShotGun")
            {
                collision.GetComponent<EnemyHealth>().damageAmount = enemyDamage[4];
                Destroy(gameObject);
            }
        }
    }
}
