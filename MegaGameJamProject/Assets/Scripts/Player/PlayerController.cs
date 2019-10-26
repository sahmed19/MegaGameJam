using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class Movement {
        public float speed;
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
    }

    public Movement movement;
    public AnimationAndTurning animation;

    private CursorController cursor;

    public GameObject dashTarget;

    void Start()
    {
        movement.rigidbody2D = GetComponent<Rigidbody2D>();
        animation.spriteRenderer = GetComponent<SpriteRenderer>();
        cursor = CursorController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(movement.canMove) {
            GatherInput();
        }
        FacingDirection();       
    }

    void FixedUpdate() {
        Locomotion();
        
    }

    void GatherInput() {
        movement.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetButtonDown("Fire2")) {
            StartCoroutine(Dash());
        }

    }

    void FacingDirection() {
        movement.facingRight = cursor.transform.position.x > transform.position.x;
        animation.spriteRenderer.flipX = !movement.facingRight;
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

        for(int i = 0; i < dashIterations; i++) {

            float dist = movement.dashDistance/(1.0f * dashIterations);

            Vector2 velocity = direction * dist;
            dashTarget.transform.position = transform.position + new Vector3(velocity.x, velocity.y, 0f);

            yield return new WaitForEndOfFrame();

            if(Physics2D.OverlapCircleAll(movement.rigidbody2D.position + velocity, .25f, movement.obstacles.value).Length > 0) {
                break;
            }
                        
            /*
            RaycastHit2D collisionDetector = Physics2D.Raycast(movement.rigidbody2D.position, velocity, dist, movement.obstacles.value);

            if(collisionDetector.collider != null) {
                break;
            }
            */

            movement.rigidbody2D.MovePosition(movement.rigidbody2D.position + velocity);
            

        }

        movement.input = direction;

        yield return new WaitForSeconds(.2f);

        movement.canMove = true;

    }

}
