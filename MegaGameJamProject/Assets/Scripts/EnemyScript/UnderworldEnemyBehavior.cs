using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderworldEnemyBehavior : MonoBehaviour
{
    
    public float speed;
    public float attackDistance;
    public float maxDeathTimer;
    bool facingRight;

    Animator animator;
    SpriteRenderer renderer;
    Player player;

    bool canMove = true;

    float deathTimer;

    void Start() {
        
        player = Player.INSTANCE;
        deathTimer = maxDeathTimer;
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update() {

        if(player.PlayerInUnderworld()) {
            if(canMove) MoveAndAttackPlayer();
            deathTimer = maxDeathTimer;        
        } else {
            deathTimer -= Time.deltaTime;

            if(deathTimer < 0f) {
                Destroy(gameObject);
            }

        }


    }

    void MoveAndAttackPlayer() {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        float distance = (transform.position - player.transform.position).sqrMagnitude;

        if(distance > attackDistance) {
            transform.position += direction * speed * Time.deltaTime;
        } else {
            animator.SetTrigger("Attack");
        }

    }

    void Attack() {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + new Vector3(.6f, -.1f), .35f);

        foreach(Collider2D collider in colliders) {
            if(collider.CompareTag("Player")) {

                player.TakeDamage(100);

            }
        }

    }

    void SetCanMove(int c) {
        canMove = (c>0);
    }

}
