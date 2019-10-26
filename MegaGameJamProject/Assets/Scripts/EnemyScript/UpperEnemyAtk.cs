﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperEnemyAtk : MonoBehaviour
{

    //Enemy Attack fields
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 25;


    //Reference to playerHealth in Player
    Player playerHealth;

    //Reference to Enemy HP in UpperEnemyHP
    UpperEnemyHP enemyHealth;

    //Reference to GO player
    GameObject player;

    //In Range?
    bool playerInRange;

    //Attack timer
    float timer;


    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player>();
        enemyHealth = GetComponent<UpperEnemyHP>();

    }

    //Player can get hit by enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        //COLLIDER for the enemy range
        if (other.gameObject == player)
        {
            //Player is within X ft
            playerInRange = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }

}