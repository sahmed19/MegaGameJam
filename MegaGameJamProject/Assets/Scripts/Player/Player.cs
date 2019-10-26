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


    /*Damage Image & vars

    public ImageConversion damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);

    */

        //Dead or Damaged bools
    bool isDead;
    bool damaged;

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


}
