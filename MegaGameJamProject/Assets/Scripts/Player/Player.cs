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

    public PitfallDetector pitfallDetector;
    bool inPitfall = false;
    float pitfallScore = 0;

    CircleCollider2D collider2D;

        //Establishes Player Health
    void Awake()
    {
        INSTANCE = this;
        currentHealth = startingHealth;
    }

    void Start() {
        controller = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    public bool PlayerInUnderworld() {
        return isFlipped;
    }

    // Update is called once per frame
    void Update()
    {

        Pitfallin();

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
        isDead = true;
        //currentHealth = startingHealth;
        //Checkpoint.RespawnPlayer();

        //Turn off player movement
        //Death sequence
        CameraFollow.INSTANCE.Death();

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
                hpComponent.TakeDamage(0);
                somethingHit = true;
            }

            EnemyMeleeBehavior meleeBehavior = colliders[rep].GetComponent<EnemyMeleeBehavior>();
            EnemyRangedBehavior rangedBehavior = colliders[rep].GetComponent<EnemyRangedBehavior>();

            if(meleeBehavior != null) {
                meleeBehavior.AddToVelocity(12f * (controller.movement.facingRight? Vector3.right : Vector3.left));
                somethingHit = true;
            }

            if(rangedBehavior != null) {
                rangedBehavior.AddToVelocity(12f * (controller.movement.facingRight? Vector3.right : Vector3.left));
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
            SoundFXManager.instance.PlaySound("FX", "Hit_Small");
        }

        CameraFollow.INSTANCE.ShakeScreen(.3f);

    }

    void HeavyAttack() {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(
        new Vector2(transform.position.x + .75f * (controller.movement.facingRight? 1f : -1f), transform.position.y), new Vector2(1f, .7f), 0f);
        
        bool somethingHit = false;
        for(int rep = 0; rep < colliders.Length; rep++)
        {

            EnemyHP hpComponent = colliders[rep].GetComponent<EnemyHP>();

            if (hpComponent != null)
            {
                hpComponent.TakeDamage(1);
                somethingHit = true;
            }

            EnemyMeleeBehavior meleeBehavior = colliders[rep].GetComponent<EnemyMeleeBehavior>();
            EnemyRangedBehavior rangedBehavior = colliders[rep].GetComponent<EnemyRangedBehavior>();

            if(meleeBehavior != null) {
                meleeBehavior.AddToVelocity(1f * (controller.movement.facingRight? Vector3.right : Vector3.left));
                somethingHit = true;
            }

            if(rangedBehavior != null) {
                rangedBehavior.AddToVelocity(1f * (controller.movement.facingRight? Vector3.right : Vector3.left));
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
            SoundFXManager.instance.PlaySound("FX", "Hit_Small");
        }

        CameraFollow.INSTANCE.ShakeScreen(1f);

    }

    //Method to flip the world of the player by operation on y
    void FlipWorlds()
    {
        
        controller.animation.animator.SetBool("Ghost", !isFlipped);
        SoundFXManager.instance.PlaySound("FX", "Bell", false, .5f, Random.Range(.8f, 1.2f));
        CameraFollow.INSTANCE.FlipWorld();

        if (!isFlipped)
        {
            SoundFXManager.instance.PlaySound("Ambience", "Wind1", true, .3f);
            controller.movement.dashCount = 3;
            collider2D.radius = .25f;
            transform.position += Vector3.down * 100f;
            isFlipped = true;
            spark.transform.position = transform.position;
            spark.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
        }

        else
        {
            SoundFXManager.instance.StopSound("Ambience", "Wind1");
            controller.movement.dashCount = 0;
            collider2D.radius = .5f;
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
    void Pitfallin() {



        //pitfallDetector.transform.position = (isFlipped? transform.position + Vector3.up * 100f : transform.position);

        inPitfall = pitfallDetector.IsInPitfall();

        if(inPitfall) {
        Debug.Log("IN PITFALL!");
        }

        //pitfallBlack.enabled = inPitfall;

        if(inPitfall && !PlayerInUnderworld()) {
            
            pitfallScore += .3f * Time.deltaTime;

            if(pitfallScore > .3f) {
                controller.movement.canMove = false;
            }

            Mathf.Clamp01(pitfallScore);
            transform.localScale = Vector3.one * Mathf.Round((1f-pitfallScore)*16f)/16f;
            
            //controller.movement.velocity += (pitfallScore * Vector2.down);

            if(pitfallScore >= .99f) {
                TakeDamage(5);
            }

        } else {
            transform.localScale = Vector3.one;
            pitfallScore = 0f;
        }

    }

    public int DashCount() {return controller.movement.dashCount;}

    IEnumerator FlashForDamage() {

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.3f);
        spriteRenderer.color = Color.white;

    }

}
