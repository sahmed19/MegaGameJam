using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperEnemyHP : MonoBehaviour
{

    //Health values
    public int startingHealth = 100;
    public int currentHealth;

    //Enemy is dead?
    bool isDead;

    void Awake()
    {
        //Equate HP
        currentHealth = startingHealth;
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy = dead af
        if (isDead) return;

        // Take hit
        currentHealth -= amount;

        // If enemy dies
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);

        // Enemy cant be hit??
        //  capsuleCollider.isTrigger = true;

    }

}

