using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Enum of attacks
public enum attackTypes
{
    snowBall,
    icicle, 
    snowGun,
    deadFish,
    snowCluster
}

public class PlayerShooting : MonoBehaviour
{

    const int TOTALATTACKS = 5;

    [HideInInspector]
    public attackTypes attack; //Create attackTypes

    [HideInInspector]
    public int currAttack; //What the current attack is

    public float[] attackRates = new float[TOTALATTACKS]; //Array of all the attack rates

    public float[] bulletSpeed = new float[TOTALATTACKS];

    private float timeToNextAttack = 0; //How much longer until player can attack again

    public GameObject firePoint; //Where the bullet spawns from

    public GameObject[] bullets = new GameObject[TOTALATTACKS]; //The different bullets to be spawned

    public float snowGunSpread;

    private Vector3 mousePos,
                    adjustedPos,
                    randomPos;

    private float randomX,
                  randomY;

    // Start is called before the first frame update
    void Start()
    {
        attack = attackTypes.snowBall;
        currAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeToNextAttack)
        {
            changeAttack();
        }

        if (Input.GetMouseButton(0) && Time.time > timeToNextAttack)
        {
            mousePos = Input.mousePosition;
            adjustedPos = Camera.main.ScreenToWorldPoint(mousePos);
            adjustedPos.z = 0;
            Debug.Log(adjustedPos);
            switch (attack)
            {
                case attackTypes.snowBall:
                    fireSnowball(attackRates[0], adjustedPos);
                    break;
                case attackTypes.icicle:
                    fireIcicle(attackRates[1], adjustedPos);
                    break;
                case attackTypes.snowGun:
                    fireSnowGun(attackRates[2], adjustedPos);
                    break;
                case attackTypes.deadFish:
                    fireDeadFish(attackRates[3], adjustedPos);
                    break;
                case attackTypes.snowCluster:
                    fireSnowCluster(attackRates[4], adjustedPos);
                    break;

            }
        }
    }

    //Fires Snowball in direction of mouse location
    void fireSnowball(float attackRate, Vector3 mouseLoc)
    {   
        timeToNextAttack = Time.time + attackRate;
        GameObject bullet = Instantiate(bullets[0], firePoint.transform.position, firePoint.transform.rotation);
        bullet.transform.right = mouseLoc - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[0] * bullet.transform.right;
        Destroy(bullet, 5f);
    }

    /*Fires Incicle in direction of mouse location
      Improved Version of the Snowball    
    */
    void fireIcicle(float attackRate, Vector3 mouseLoc)
    {
        timeToNextAttack = Time.time + attackRate;
        GameObject bullet = Instantiate(bullets[1], firePoint.transform.position, firePoint.transform.rotation);
        bullet.transform.right = adjustedPos - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[1] * bullet.transform.right;
        Destroy(bullet, 5f);
    }

    void fireSnowGun(float attackRate, Vector3 mouseLoc)
    {
        timeToNextAttack = Time.time + attackRate;
        randomX = Random.Range(-snowGunSpread, snowGunSpread);
        randomY = Random.Range(-snowGunSpread, snowGunSpread);
        randomPos = new Vector3(adjustedPos.x + randomX, adjustedPos.y + randomY, 0);
        GameObject bullet = Instantiate(bullets[2], firePoint.transform.position, firePoint.transform.rotation);
        bullet.transform.right = randomPos - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[2] * bullet.transform.right;
        Destroy(bullet, 2.5f);

    }

    void fireDeadFish(float attackRate, Vector3 mouseLoc)
    {
        timeToNextAttack = Time.time + attackRate;
        GameObject bullet = Instantiate(bullets[3], firePoint.transform.position, firePoint.transform.rotation);
        bullet.transform.right = adjustedPos - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[3] * bullet.transform.right;
    }

    void fireSnowCluster(float attackRate, Vector3 mouseLoc)
    {
        timeToNextAttack = Time.time + attackRate;
    }

    void changeAttack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            switch (currAttack)
            {
                case 0:
                    attack = attackTypes.icicle;
                    currAttack = 1;
                    break;
                case 1:
                    attack = attackTypes.snowGun;
                    currAttack = 2;
                    break;
                case 2:
                    attack = attackTypes.deadFish;
                    currAttack = 3;
                    break;
                case 3:
                    attack = attackTypes.snowCluster;
                    currAttack = 4;
                    break;
                case 4:
                    attack = attackTypes.snowBall;
                    currAttack = 0;
                    break;
            }
        }
    }



}
