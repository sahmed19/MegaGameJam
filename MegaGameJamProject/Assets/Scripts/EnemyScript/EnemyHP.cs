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

    public LayerMask pitfall;
    //public PitfallDetector pitfallDetector;
    bool inPitfall;
    float pitfallScore = 0f;

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

    void Update() {

        
        RaycastHit2D pd = Physics2D.Raycast(transform.position, Vector3.down, .03f, pitfall.value);
        
        if(pd.collider != null) {
            Debug.Log("Iscalling");

            if(myMelee != null) {
                myMelee.enabled = false;
            } else if(myRanged != null) {
                myRanged.enabled = false;
            }

            pitfallScore += .6f * Time.deltaTime;

            pitfallScore = Mathf.Clamp01(pitfallScore);
            transform.localScale = Vector3.one * Mathf.Round((1f-pitfallScore)*16f)/16f;

            if(pitfallScore >= .99f) {
                Death(true);
            }

        }
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

    void Death(bool destroy = false)
    {
        // The enemy is dead.
        isDead = true;

        Debug.Log("Homie down");

        collider2D.enabled = false;

        SpriteRenderer corpsesp = Instantiate(corpse, transform.position + Vector3.down * 100f, Quaternion.identity);
        corpsesp.flipX = spriteRenderer.flipX;

        if(destroy) {
            Destroy(gameObject);
        }

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

    public void SetInPitfall(bool p) {
        inPitfall = p;
    }

}

