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
    }

    [System.Serializable]
    public class Animation {
        public SpriteRenderer spriteRenderer;
    }

    public Movement movement;
    public Animation animation;

    private CursorController cursor;

    void Start()
    {
        movement.rigidbody2D = GetComponent<Rigidbody2D>();
        animation.spriteRenderer = GetComponent<SpriteRenderer>();
        cursor = CursorController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        FacingDirection();       
    }

    void FixedUpdate() {
        Locomotion();
    }

    void GatherInput() {
        movement.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FacingDirection() {
        movement.facingRight = cursor.transform.position.x > transform.position.x;
        animation.spriteRenderer.flipX = !movement.facingRight;
    }

    void Locomotion() {

        Vector2 targetVelocity = movement.input.normalized * movement.speed * Time.fixedDeltaTime;

        movement.velocity = Vector2.Lerp(movement.velocity, targetVelocity, movement.smoothingSpeed * Time.fixedDeltaTime);

        movement.rigidbody2D.MovePosition(movement.rigidbody2D.position + movement.velocity);

    }
}
