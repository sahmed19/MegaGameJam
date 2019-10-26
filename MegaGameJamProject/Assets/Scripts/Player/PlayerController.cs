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

        public CharacterController2D controller2D;
    }

    public Movement movement;

    void Start()
    {
        movement.controller2D = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Input() {
        movement.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Locomotion() {

        

    }
}
