using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class Movement {
        [HideInInspector]
        public float speed;
        public float overworldSpeed = 100;
        public float underworldSpeed = 300;
        public float smoothingSpeed;
        public bool facingRight;
        
        public Vector2 input;
        public Rigidbody2D rigidbody2D;
        public Vector2 velocity;

        public bool canUseDash = true;
        public float dashDistance;
        public float dashSpeed;

        public bool canMove = true;
        public LayerMask obstacles;
    }

    [System.Serializable]
    public class AnimationAndTurning {
        public SpriteRenderer spriteRenderer;
        public Animator animator;
    }

    public Movement movement;
    public AnimationAndTurning animation;

    private CursorController cursor;

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
        movement.rigidbody2D = GetComponent<Rigidbody2D>();
        animation.spriteRenderer = GetComponent<SpriteRenderer>();
        animation.animator = GetComponent<Animator>();
        cursor = CursorController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        UnderworldMechanics();
        if(movement.canMove) {
            GatherInput();
        }
        FacingDirection();
        AnimatingPlayer();
    }

    void FixedUpdate() {
        Locomotion();
    }

    void UnderworldMechanics() {
        bool inUnderworld = player.PlayerInUnderworld();

        //Can only dash in underworld
        movement.canUseDash = inUnderworld;

        movement.speed = inUnderworld? movement.underworldSpeed : movement.overworldSpeed;

    }

    void GatherInput() {
        movement.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        
        if(movement.canUseDash && Input.GetButtonDown("Fire2")) {
            StartCoroutine(Dash());
        }

        if(Input.GetButtonDown("Fire1")) {
            animation.animator.SetTrigger("Attack");
        }

        if(Input.GetButtonDown("FlipWorld")) {
            animation.animator.SetTrigger("FlipWorld");
        }

    }

    public void SetCanMove(int c) {
        movement.canMove = (c > 0);
        if(c == 0) {
            movement.input = Vector2.zero;
        }
    }

    void FacingDirection() {
        movement.facingRight = cursor.transform.position.x > transform.position.x;
        animation.spriteRenderer.flipX = !movement.facingRight;
    }

    void AnimatingPlayer() {
        animation.animator.SetFloat("Speed", Mathf.Clamp01(movement.input.sqrMagnitude));
    }

    void Locomotion() {

        Vector2 targetVelocity = movement.input.normalized * movement.speed * Time.fixedDeltaTime;
        movement.velocity = Vector2.Lerp(movement.velocity, targetVelocity, movement.smoothingSpeed * Time.fixedDeltaTime);
        movement.rigidbody2D.velocity = movement.velocity;

    }

    IEnumerator Dash() {

        movement.canMove = false;

        movement.input = Vector2.zero;
        movement.rigidbody2D.velocity = Vector2.zero;

        Vector2 direction = (cursor.transform.position - transform.position).normalized;

        int dashIterations = 5;
        float dist = movement.dashDistance/(1.0f * dashIterations);

        for(int i = 0; i < dashIterations; i++) {

            Vector2 velocity = direction * dist;

            velocity.y *= 0.5f;

            yield return new WaitForSeconds(.01f);

            if(Physics2D.OverlapCircleAll(movement.rigidbody2D.position + velocity, .25f, movement.obstacles.value).Length > 0) {
                break;
            }
                        
            /*
            RaycastHit2D collisionDetector = Physics2D.Raycast(movement.rigidbody2D.position, velocity, dist, movement.obstacles.value);

            if(collisionDetector.collider != null) {
                break;
            }
            */

            transform.position += new Vector3(velocity.x, velocity.y, 0f);
            //movement.rigidbody2D.MovePosition(movement.rigidbody2D.position + velocity);
            

        }

        movement.input = direction;

        yield return new WaitForSeconds(.05f);

        movement.canMove = true;

    }

}
