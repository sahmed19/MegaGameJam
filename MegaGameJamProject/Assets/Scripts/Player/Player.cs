using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;


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
    bool damaged;
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

        //If Player takes damage
        if (damaged)
        {
            // damageImage.color = flashColor
        }
        else
        {
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            //ttack();
        }


        //Reset damaged flad
        damaged = false;


    }

    public void TakeDamage(int amount) {TakeDamage(amount, Vector2.zero);}

    public void TakeDamage(int amount, Vector2 pushback)
    {
        //Flag Damage
        damaged = true;

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
        for(int rep = 0; rep < colliders.Length; rep++)
        {
            EnemyHP hpComponent = colliders[rep].GetComponent<EnemyHP>();

            if (hpComponent != null)
            {
                hpComponent.TakeDamage(50);
            }

            EnemyMeleeBehavior meleeBehavior = colliders[rep].GetComponent<EnemyMeleeBehavior>();

            if(meleeBehavior != null) {
                meleeBehavior.AddToVelocity(4f * (controller.movement.facingRight? Vector3.right : Vector3.left));
            }

            Statue statue = colliders[rep].GetComponent<Statue>();
            
            if(statue != null) {
                statue.Break();
            }

        }

    }

    //Method to flip the world of the player by operation on y
    void FlipWorlds()
    {

        CameraFollow.INSTANCE.FlipWorld();

        if (!isFlipped)
        {
            transform.position += Vector3.down * 100f;
            isFlipped = true;
        }

        else
        {

            transform.position += Vector3.up * 100f;
            isFlipped = false;

        }

    }

    IEnumerator FlashForDamage() {

        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.3f);
        spriteRenderer.color = Color.white;

    }

}
