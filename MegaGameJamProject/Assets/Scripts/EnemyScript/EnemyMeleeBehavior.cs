using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBehavior : MonoBehaviour
{

    public int attackDamage = 25;
    public float seeDistance = 5f;

    //Reference to player in Player
    Player player;

    //Reference to Enemy HP in UpperEnemyHP
    EnemyHP enemyHealth;

    Animator animator;
    SpriteRenderer renderer;

    Vector3 velocity;

    public bool facingRight;

    public LayerMask sightlineMask;
    public float speed;

    Vector3 direction;

    public float returnTimer;

    void Start()
    {
        
        // Setting up the references.
        player = Player.INSTANCE;
        enemyHealth = GetComponent<EnemyHP>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.flipX = !facingRight;
    }


    // Update is called once per frame
    void Update()
    {

        if(Player.INSTANCE.PlayerInUnderworld()) {
            returnTimer = 1.0f;
        }
        
        if(returnTimer > 0) {
            returnTimer -= Time.deltaTime;
        }

        if(!enemyHealth.isDead && returnTimer <= 0f) {
            
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
            if(collider.CompareTag("Player"))
            {
                return true;   
            }
        }

        return false;
    }

    public void Attack()
    {

        if(PlayerInRange()) {

            player.TakeDamage(attackDamage, Vector2.right * 5.0f);
            
        }
    }

    public void FacePlayer() {
        facingRight = direction.x > 0;
        renderer.flipX = !facingRight;
    }

    bool FacingPlayer() {
        //return (facingRight && (direction.x > 0)) || (!facingRight && (direction.x < 0));

        Vector3 directable = facingRight? Vector3.right : Vector3.left;

        return (Vector3.Dot(direction, directable) > -.3f);

    }

    void Pathfinding() {

        direction = (player.transform.position - transform.position).normalized;
        
        RaycastHit2D sightline = Physics2D.Raycast(transform.position, direction, seeDistance, sightlineMask.value);



        if(FacingPlayer() && sightline.collider != null && sightline.collider.CompareTag("Player")) {
            
            FacePlayer();

            transform.position += direction * speed * Time.deltaTime;

            animator.SetFloat("Speed", 1f);            

        } else {
            animator.SetFloat("Speed", 0f);
        }

    }

}
