using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBehavior : MonoBehaviour
{
    public Transform firePoint;

    //Enemy Attack fields
    public float timeBetweenAttacks = 0.5f;

    public Vector2 direction;

    //Reference to Projectile
    public GameObject arrowPrefab;

    //Reference to player in Player
    Player player;

    //Reference to Enemy HP in UpperEnemyHP
    EnemyHP enemyHealth;

    //In Range?
    bool playerInRange;

    //
    public LayerMask sightlineMask;

    //Attack timer
    float timer;


    void Start()
    {
        // Setting up the references.
        player = Player.INSTANCE;
        enemyHealth = GetComponent<EnemyHP>();

    }

    //Player can get hit by enemy
    void OnTriggerEnter2D(Collider2D other)
    {
        //COLLIDER for the enemy range
        if (other.gameObject == player.gameObject)
        {
            //Player is within X ft
            playerInRange = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

        //Raycast for Aggro
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 10f, sightlineMask.value);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            //Aggro

        }

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        //Instantiate instance of arrow projectile
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}
