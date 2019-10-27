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
    SpriteRenderer renderer;

    Vector3 velocity;

    bool facingRight;

    public LayerMask sightlineMask;
    public float speed;

    bool xFirst = false;

    Vector3 direction;

    void Start()
    {
        // Setting up the references.
        player = Player.INSTANCE;
        enemyHealth = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();

    }


    // Update is called once per frame
    void Update()
    {
        
        if(!enemyHealth.isDead) {

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            
            if(PlayerInRange()) {
                animator.SetTrigger("Attack");
            }

            transform.position += velocity * Time.deltaTime;
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * 7.0f);

            Pathfinding();
        }
    }

    public void AddToVelocity(Vector3 v) {
        velocity += v;
    }

    bool PlayerInRange() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + Vector3.right * .25f * (facingRight? 1f : -1f), .25f);

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

    public void FacePlayer() {
        facingRight = direction.x > 0;
        renderer.flipX = !facingRight;
    }

    bool FacingPlayer() {
        return (facingRight && (direction.x > 0)) || (!facingRight && (direction.x < 0));
    }

    void Pathfinding() {

        direction = (player.transform.position - transform.position).normalized;
        
        RaycastHit2D sightline = Physics2D.Raycast(transform.position, direction, 10f, sightlineMask.value);



        if(FacingPlayer() && sightline.collider != null && sightline.collider.CompareTag("Player")) {
            
            FacePlayer();

            transform.position += direction * speed * Time.deltaTime;

            animator.SetFloat("Speed", 1f);            

        } else {
            animator.SetFloat("Speed", 0f);
        }

    }

}
