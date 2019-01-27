using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Image healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public GameManager gm;


    Animator anim;
    PlayerWalk playerMovement;
    PlayerShooting playerShooting;
    public bool damaged = false;
    bool isDead;



    void Awake()
    {
        currentHealth = startingHealth; 
        playerMovement = GetComponent <PlayerWalk> ();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            //damageImage.color = flashColor;
        }
        else
        {
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
               
        currentHealth -= amount;

        healthSlider.rectTransform.localScale.Set(currentHealth, transform.localScale.y, transform.localScale.z);
                
        //healthSlider.transform.localScale.Set(currentHealth, transform.localScale.y, transform.localScale.z);

        if (currentHealth <= 0 && !isDead)
        {
           Death();
        }


       
    }

    void Death()
    {
        gm.isDead = true;

        //playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        playerMovement.enabled = false;

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
} 
