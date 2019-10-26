using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBehavior : MonoBehaviour
{

    //Enemy Attack fields
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 25;
    public Vector2 direction;

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

    Animator animator;

    void Start()
    {
        // Setting up the references.
        player = Player.INSTANCE;
        enemyHealth = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {

        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + Vector3.right * .25f, .25f);

        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Player") && timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0)
            {


                animator.SetTrigger("Attack");
                //Attack();    
            }
        }

        
    }

    public void Attack()
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
