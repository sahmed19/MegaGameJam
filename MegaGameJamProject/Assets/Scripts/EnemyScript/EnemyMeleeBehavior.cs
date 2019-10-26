using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBehavior : MonoBehaviour
{

    //Enemy Attack fields
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 25;
    public Vector2 direction;

    //Reference to playerHealth in Player
    Player player;

    //Reference to Enemy HP in UpperEnemyHP
    EnemyHP enemyHealth;

    //Reference to GO player
    GameObject playerObject;

    //In Range?
    bool playerInRange;

    //
    public LayerMask sightlineMask;

    //Attack timer
    float timer;


    void Awake()
    {
        // Setting up the references.
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        enemyHealth = GetComponent<EnemyHP>();

    }

    //Player can get hit by enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        //COLLIDER for the enemy range
        if (other.gameObject == playerObject)
        {
            //Player is within X ft
            playerInRange = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //Raycast for Aggro
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerObject.transform.position - transform.position, 10f, sightlineMask.value);
        if(hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Aggro

        }

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
        if (player.currentHealth > 0)
        {
            // ... damage the player.
            player.TakeDamage(attackDamage);
        }
    }

}
