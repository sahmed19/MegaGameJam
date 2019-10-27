using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBehavior : MonoBehaviour
{

    public float seeDistance = 2f;
    public float shootDistance = 1f;
    public float speed;

    public Transform firePoint;

    public Projectile arrowPrefab;

    Player player;

    EnemyHP enemyHealth;

    
    Animator animator;
    SpriteRenderer renderer;

    Vector3 direction;
    Vector3 velocity;
    //
    public LayerMask sightlineMask;

    //Attack timer
    float timer;

    public bool facingRight = true;

    bool canMove = true;

    float firepointx;
    float firepointy;

    void Start()
    {

        // Setting up the references.
        player = Player.INSTANCE;
        enemyHealth = GetComponent<EnemyHP>();
        player = Player.INSTANCE;
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.flipX = !facingRight;

        firepointx = firePoint.transform.localPosition.x;
        firepointy = firePoint.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemyHealth.isDead) {

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            
            if(PlayerInRange() && FacingPlayer()) {
                animator.SetTrigger("Attack");
            } else if(canMove) {
                Pathfinding();
            }

            transform.position += velocity * Time.deltaTime;
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * 7.0f);

        }
    }

    public void SetCanMove(int c) {
        canMove = c > 0;
    }

    public void AddToVelocity(Vector3 v) {
        velocity += v;
    }

    bool PlayerInRange() {
        RaycastHit2D sightline = Physics2D.Raycast(transform.position, direction, shootDistance, sightlineMask.value);

        if(sightline.collider != null && sightline.collider.CompareTag("Player"))
        {
            return true;   
        }

        return false;
    }

    public void FacePlayer() {
        facingRight = direction.x > 0;
        renderer.flipX = !facingRight;

        firePoint.transform.localPosition = new Vector3(firepointx * (facingRight? 1.0f : -1.0f), firepointy, 0f);
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

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;

        //Instantiate instance of arrow projectile
        Projectile projectile = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);

        projectile.direction = (player.transform.position - transform.position).normalized;
        projectile.layermaskValue = sightlineMask.value;
    }
}
