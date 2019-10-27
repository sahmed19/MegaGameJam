using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;

    public GameObject spark;

    public static Player INSTANCE;

    PlayerController controller;
    SpriteRenderer spriteRenderer;

    /*
    Damage Image & vars

    public ImageConversion damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    */

        //Dead or Damaged bools
    bool isDead;
    bool isFlipped;

        //Establishes Player Health
    void Awake()
    {
        INSTANCE = this;
        currentHealth = startingHealth;
    }

    void Start() {
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool PlayerInUnderworld() {
        return isFlipped;
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void TakeDamage(int amount) {TakeDamage(amount, Vector2.zero);}

    public void TakeDamage(int amount, Vector2 pushback)
    {
        controller.movement.velocity += pushback;

        //Damage taken
        currentHealth -= amount;

        StartCoroutine(FlashForDamage());

        //Player dies
        if (currentHealth <= 0 && !isDead) Death();

    }

    //Death Method
    public void Death()
    {

        currentHealth = startingHealth;
        Checkpoint.RespawnPlayer();

        //Turn off player movement
        //Death sequence


    }

    void Attack()
    {
        //Bruh
        //Checks all colliders in circle of radius size 1f right now
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            new Vector2(transform.position.x + .5f * (controller.movement.facingRight? 1f : -1f), transform.position.y), .25f);
        
        bool somethingHit = false;
        
        for(int rep = 0; rep < colliders.Length; rep++)
        {
            EnemyHP hpComponent = colliders[rep].GetComponent<EnemyHP>();

            

            if (hpComponent != null)
            {
                hpComponent.TakeDamage(50);
                somethingHit = true;
            }

            EnemyMeleeBehavior meleeBehavior = colliders[rep].GetComponent<EnemyMeleeBehavior>();

            if(meleeBehavior != null) {
                meleeBehavior.AddToVelocity(4f * (controller.movement.facingRight? Vector3.right : Vector3.left));
                somethingHit = true;
            }

            Statue statue = colliders[rep].GetComponent<Statue>();
            
            if(statue != null) {
                statue.Break();
                somethingHit = true;
            }

        }

        if(somethingHit) {
            CameraFollow.INSTANCE.ShakeScreen(1f);
        }

        CameraFollow.INSTANCE.ShakeScreen(.3f);

    }

    void HeavyAttack() {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
        new Vector2(transform.position.x + .75f * (controller.movement.facingRight? 1f : -1f), transform.position.y), new Vector2(1f, .5f), 0f);
        
        bool somethingHit = false;
        for(int rep = 0; rep < colliders.Length; rep++)
        {

            EnemyHP hpComponent = colliders[rep].GetComponent<EnemyHP>();

            if (hpComponent != null)
            {
                hpComponent.TakeDamage(100);
                somethingHit = true;
            }

            EnemyMeleeBehavior meleeBehavior = colliders[rep].GetComponent<EnemyMeleeBehavior>();

            if(meleeBehavior != null) {
                meleeBehavior.AddToVelocity(4f * (controller.movement.facingRight? Vector3.right : Vector3.left));
                somethingHit = true;
            }

            Statue statue = colliders[rep].GetComponent<Statue>();
            
            if(statue != null) {
                statue.Break();
                somethingHit = true;
            }

            if(somethingHit) {
                
            }

        }

        if(somethingHit) {
            CameraFollow.INSTANCE.ShakeScreen(1f);
        }

        CameraFollow.INSTANCE.ShakeScreen(1f);

    }

    //Method to flip the world of the player by operation on y
    void FlipWorlds()
    {
        
        controller.animation.animator.SetBool("Ghost", !isFlipped);

        CameraFollow.INSTANCE.FlipWorld();

        if (!isFlipped)
        {
            transform.position += Vector3.down * 100f;
            isFlipped = true;
            spark.transform.position = transform.position;
            spark.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
        }

        else
        {
            transform.position += Vector3.up * 100f;
            isFlipped = false;
        }

    }

    public float DistanceFromSpark() {

        if(!PlayerInUnderworld()) {
            return -1f;
        } else {
            return (spark.transform.position - transform.position).sqrMagnitude;
        }

    }

    IEnumerator FlashForDamage() {

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.3f);
        spriteRenderer.color = Color.white;

    }

}
