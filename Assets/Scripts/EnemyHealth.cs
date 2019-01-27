using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyType
{
    seal,
    orca
}

public class EnemyHealth : MonoBehaviour
{
    public int currEnemy = 0; //0 for seal, 1 for orca
    private enemyType enemy;

    [HideInInspector]
    public int health;

    public int[] enemyHealth = new int[2];

    [HideInInspector]
    public bool hit = false;

    [HideInInspector]
    public int damageAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (currEnemy == 0)
        {
            enemy = enemyType.seal;
            health = enemyHealth[0];
        }
        else
        {
            enemy = enemyType.orca;
            health = enemyHealth[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            damage(damageAmount);
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }

    void damage(int damage)
    {
        health -= damage;
        hit = false;
    }

}
