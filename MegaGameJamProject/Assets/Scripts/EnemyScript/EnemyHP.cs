using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{

    //Health values
    public int startingHealth = 100;
    public int currentHealth;

    //Enemy is dead?
    bool isDead;

    SpriteRenderer spriteRenderer;
    
    public ParticleSystem bloodParticles;

    void Awake()
    {
        //Equate HP
        currentHealth = startingHealth;
    }

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        // If the enemy = dead af
        if (isDead) return;

        // Take hit
        currentHealth -= amount;
        StartCoroutine(FlashForDamage());
        
        if(bloodParticles != null) {
            bloodParticles.Play();
        }
        
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

        Debug.Log("Homie down");

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);

        // Enemy cant be hit??
        //  capsuleCollider.isTrigger = true;

    }

    IEnumerator FlashForDamage() {

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.3f);
        spriteRenderer.color = Color.white;

    }

}

