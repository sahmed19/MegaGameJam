using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 20f;
    public int attackDamage = 25;

    public Rigidbody2D arrow;
    //Reference to player in Player


    Player player;

    // Start is called before the first frame update
    void Start()
    {
        arrow.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        Destroy(gameObject);
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(attackDamage);
        }

    }
}
