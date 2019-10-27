using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBehavior : MonoBehaviour
{

    //Enemy Attack fields
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 25;

    //Reference to player in Player
    Player player;

    //Reference to Enemy HP in UpperEnemyHP
    EnemyHP enemyHealth;

    //Attack timer
    float timer;

    Animator animator;

    Vector3 velocity;

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

        if(PlayerInRange()) {
            animator.SetTrigger("Attack");
        }

        transform.position += velocity * Time.deltaTime;
        
        velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * 7.0f);

    }

    public void AddToVelocity(Vector3 v) {
        velocity += v;
    }

    bool PlayerInRange() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + Vector3.right * .25f, .25f);

        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Player") && timer >= timeBetweenAttacks)
            {
                return true;   
            }
        }

        return false;
    }

    public void Attack()
    {

        if(PlayerInRange()) {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (player.currentHealth > 0)
            {
                // ... damage the player.
                player.TakeDamage(attackDamage, Vector2.right * 5.0f);
            }
        }
    }

}
