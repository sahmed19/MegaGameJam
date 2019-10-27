using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{

    //Health values
    public int startingHealth = 100;
    public int currentHealth;

    //Enemy is dead?
    public bool isDead;

    SpriteRenderer spriteRenderer;
    
    public ParticleSystem bloodParticles;

    EnemyMeleeBehavior myMelee;
    EnemyRangedBehavior myRanged;
    Animator animator;
    BoxCollider2D collider2D;

    public SpriteRenderer corpse;

    void Awake()
    {
        //Equate HP
        currentHealth = startingHealth;
    }

    void Start() {
        myMelee = GetComponent<EnemyMeleeBehavior>();
        myRanged = GetComponent<EnemyRangedBehavior>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(int amount)
    {
        if(myMelee != null) {
            myMelee.FacePlayer();
        } else if(myRanged != null) {
            myRanged.FacePlayer();
        }

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

        collider2D.enabled = false;

        SpriteRenderer corpsesp = Instantiate(corpse, transform.position + Vector3.down * 100f, Quaternion.identity);
        corpsesp.flipX = spriteRenderer.flipX;

        bloodParticles.Emit(15);

        animator.SetTrigger("Death");

        // Enemy cant be hit??
        //  capsuleCollider.isTrigger = true;

    }

    IEnumerator FlashForDamage() {

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.3f);
        spriteRenderer.color = Color.white;

    }

}

