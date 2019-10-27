using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 2f;
    public int attackDamage = 25;
    //Reference to player in Player


    public float lifeTime;    
    public Vector3 direction = Vector3.zero;

    Player player;

    public int layermaskValue = 0;

    void Start() {
        player = Player.INSTANCE;

        transform.eulerAngles = Vector3.forward * Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if(lifeTime < 0f) {
            Destroy(gameObject);
        }

        float distance = speed * Time.deltaTime;

        RaycastHit2D projectileRaycast = Physics2D.Raycast(transform.position, direction, distance, layermaskValue);

        if(projectileRaycast.collider != null)
        {

            if (projectileRaycast.collider.CompareTag("Player"))
            {
                player.TakeDamage(attackDamage);
                
            }

            Destroy(gameObject);

        }


        transform.position += distance * direction;
    }
}
