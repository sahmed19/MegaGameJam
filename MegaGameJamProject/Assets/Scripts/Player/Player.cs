using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;

    //Health Bar
    public Slider healthSlider;


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

        currentHealth = startingHealth;
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
            Attack();
        }

        if (Input.GetButtonDown("FlipWorld"))
        {
            FlipWorlds();
        }


        //Reset damaged flad
        damaged = false;


    }

    public void TakeDamage(int amount)
    {
        //Flag Damage
        damaged = true;

        //Damage taken
        currentHealth -= amount;

        //Health Slider
        healthSlider.value = currentHealth;

        //Player dies
        if (currentHealth <= 0 && !isDead) Death();

    }

    //Death Method
    void Death()
    {

        isDead = true;

        //Turn off player movement
        //Death sequence


    }

    void Attack()
    {
        //Bruh
        //Checks all colliders in circle of radius size 1f right now
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 1f);
        for(int rep = 0; rep < colliders.Length; rep++)
        {
            EnemyHP hpComponent = colliders[rep].GetComponent<EnemyHP>();

            if (hpComponent != null)
            {
                hpComponent.TakeDamage(50);
            }
        }

    }

    //Method to flip the world of the player by operation on y
    void FlipWorlds()
    {

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


}
