using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    //Does this statue have an enemy inside?
    [SerializeField] bool isEnemy;
    //Reference to enemy to instantiate
    [SerializeField] GameObject enemyPrefab;
    BoxCollider2D statueCollider;
    [SerializeField] float rubbleColliderSizeX;
    [SerializeField] float rubbleColliderSizeY;
    [SerializeField] float rubbleColliderOffsetX;
    [SerializeField] float rubbleColliderOffsetY;

    void Start()
    {
        //Atatch the breaking script to the Animator, set it to play once, enable to play on collision
        gameObject.GetComponent<Animator>().enabled = false;
        statueCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void Break()
    {
        //Play animation of breaking
        gameObject.GetComponent<Animator>().enabled = true;
        statueCollider.size = new Vector2(rubbleColliderSizeX, rubbleColliderSizeY);
        statueCollider.offset = new Vector2(rubbleColliderOffsetX, rubbleColliderOffsetY);

        //Spawn enemy if this statue contains one
        if (isEnemy)
        {
            Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
}
