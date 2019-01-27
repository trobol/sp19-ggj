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

    public GameObject firePoint; //Where the bullet spawns from

    public GameObject[] bullets = new GameObject[TOTALATTACKS]; //The different bullets to be spawned

    public int bulletCount; //Number of bullets in shotgun spread

    public float[] attackRates = new float[TOTALATTACKS]; //Array of all the attack rates

    public float[] bulletSpeed = new float[TOTALATTACKS]; //Array of all the bullet speeds

    public float snowGunSpread, //Bullet spray spread
                 bulletSpread; //Shotgun spread

    public float[] knockBacks = new float[TOTALATTACKS]; //Array of all knockback values

    public float[] shakeAmount = new float[TOTALATTACKS]; //Array of all shake values

    public float[] shakeStrength = new float[TOTALATTACKS]; //Array of how strong shake is

    public float[] shakeDuration = new float[TOTALATTACKS]; //Array of shake Duration

    private float timeToNextAttack = 0; //How much longer until player can attack again

    private float timeToShakeEnd = 0;

    private GameObject storeBullets;

    [HideInInspector]
    public int i;         

    private Vector3 mousePos, //Mouse Position when mouse is clicked
                    adjustedPos, //The adjusted mouse position
                    randomPos; //Random Spread

    private float randomX, //Random X change
                  randomY; //Random Y change

    [HideInInspector]
    public Transform camTransform; //Camera Transformation for Screenshake

    private Vector3 initialPosition; //Initial camera Position

    [HideInInspector]
    public Rigidbody2D rb2d; //Player Rigidbody

    [HideInInspector]
    public bool shaking = false; //If the screen should be shaking

    private Vector2 shakingCamLoc; //Where the camera should move to

    // Start is called before the first frame update
    void Start()
    {
        attack = attackTypes.snowBall; //Set first attack
        currAttack = 0; 
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        camTransform = Camera.main.transform;
        storeBullets = GameObject.Find("_bullets");
    }

    // Update is called once per frame
    void Update()
    {
        //If the screen should be shaking, make it shake
        if (!shaking)
        {
            initialPosition = camTransform.localPosition;
        }
        else
        {
            screenShake();
        }

        //Change Current Weapon
        if(Time.time > timeToNextAttack)
        {
            changeAttack();
        }

        //If Left Click is pushed down and attack is not on cooldown
        if (Input.GetMouseButton(0) && Time.time > timeToNextAttack)
        {
            mousePos = Input.mousePosition; //Get Mouse Position
            adjustedPos = Camera.main.ScreenToWorldPoint(mousePos); //Adjusted Position
            adjustedPos.z = 0; //Set the z to zero
            
            //Check what the current attack state is and do attack
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
                    fireSnowCluster(attackRates[4], adjustedPos, bulletCount);
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
        kickBack(knockBacks[0], mouseLoc);
        timeToShakeEnd = Time.time + shakeDuration[currAttack];
        shaking = true;
        Destroy(bullet, 5f);
    }

    /*Fires Incicle in direction of mouse location
     *Improved Version of the Snowball    
     */
    void fireIcicle(float attackRate, Vector3 mouseLoc)
    {
        timeToNextAttack = Time.time + attackRate;
        GameObject bullet = Instantiate(bullets[1], firePoint.transform.position, firePoint.transform.rotation);
        bullet.transform.right = adjustedPos - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[1] * bullet.transform.right;
        kickBack(knockBacks[1], mouseLoc);
        timeToShakeEnd = Time.time + shakeDuration[currAttack];
        shaking = true;
        Destroy(bullet, 5f);
    }
    
    /*Fast Firing snowball gun
     *Sway the bullets so they are not all on target
     */
    void fireSnowGun(float attackRate, Vector3 mouseLoc)
    {
        timeToNextAttack = Time.time + attackRate;
        randomX = Random.Range(-snowGunSpread, snowGunSpread);
        randomY = Random.Range(-snowGunSpread, snowGunSpread);
        randomPos = new Vector3(adjustedPos.x + randomX, adjustedPos.y + randomY, 0);
        GameObject bullet = Instantiate(bullets[2], firePoint.transform.position, firePoint.transform.rotation);
        bullet.transform.right = randomPos - transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[2] * bullet.transform.right;
        kickBack(knockBacks[2], mouseLoc);
        timeToShakeEnd = Time.time + shakeDuration[currAttack];
        shaking = true;
        Destroy(bullet, 2.5f);

    }

    /*Fire a dead fish with angle equal to mouse direction
     *This projectile arcs and is effected by gravity
     */
    void fireDeadFish(float attackRate, Vector3 mouseLoc)
    {
        timeToNextAttack = Time.time + attackRate;
        GameObject bullet = Instantiate(bullets[3], firePoint.transform.position, firePoint.transform.rotation);
        bullet.transform.right = adjustedPos;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[3] * bullet.transform.right;
        kickBack(knockBacks[3], mouseLoc);
        timeToShakeEnd = Time.time + shakeDuration[currAttack];
        shaking = true;
        Destroy(bullet, 2f);
    }

    /*Shotgun of fire of snowballs
     *Randomize the spread based on bulletspread
     *Can change number of bullets in the shotgun spread
     */
    void fireSnowCluster(float attackRate, Vector3 mouseLoc, int bulletCount)
    {
        timeToNextAttack = Time.time + attackRate;
        for(i = 0; i < bulletCount; i++)
        {
            randomX = Random.Range(-bulletSpread, bulletSpread);
            randomY = Random.Range(-bulletSpread, bulletSpread);
            randomPos = new Vector3(adjustedPos.x + randomX, adjustedPos.y + randomY, 0);
            GameObject bullet = Instantiate(bullets[4], firePoint.transform.position, firePoint.transform.rotation);
            bullet.transform.right = randomPos - transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed[4] * bullet.transform.right;
            kickBack(knockBacks[4], mouseLoc);
            timeToShakeEnd = Time.time + shakeDuration[currAttack];
            shaking = true;
            Destroy(bullet, 1f);
        }
    }

    //Change the currently set attack
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
                    attack = attackTypes.snowCluster;
                    currAttack = 3;
                    break;
                case 3:
                    attack = attackTypes.snowBall;
                    currAttack = 0;
                    break;
                case 4:
                    attack = attackTypes.snowBall;
                    currAttack = 0;
                    break;
            }
        }
    }

    //Knock back player in oppisite direction of where they shot
    void kickBack(float kick, Vector3 mouseLoc)
    {
        mouseLoc.z = 0;
        Vector3 push = mouseLoc - transform.position;
        rb2d.AddForce(-push * kick);

    }

    //Make the screen shake
    void screenShake()
    {
        if (Time.time > timeToShakeEnd)
        {
            camTransform.localPosition = initialPosition;
            shaking = false;
        }
        else
        {
            Debug.Log("The fuck);                                                             ");
            shakingCamLoc = new Vector2(initialPosition.x, initialPosition.y) + Random.insideUnitCircle * shakeAmount[currAttack];
            camTransform.localPosition = new Vector3(shakingCamLoc.x, shakingCamLoc.y, initialPosition.z);
        }
    }
}
