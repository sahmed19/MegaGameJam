using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VoidDeath : MonoBehaviour
{
    [SerializeField] GameObject player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log("hit");
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<TilemapCollider2D>());

            StartCoroutine(WaitForDeath(0.8f));
    }

    IEnumerator WaitForDeath(float f)
    {

        yield return new WaitForSeconds(f);
        player.GetComponent<Player>().Death();
    }
}
