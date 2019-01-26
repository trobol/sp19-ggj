﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);


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
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
               
        currentHealth -= amount;
                
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
           Death();
        }


       
    }

    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects();

        anim.SetTrigger("Die");

        playerMovement.enabled = false;

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
} 
