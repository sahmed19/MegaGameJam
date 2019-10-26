using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    //Does this statue have an enemy inside?
    [SerializeField] bool isEnemy;
    //Reference to enemy to instantiate
    [SerializeField] GameObject enemyPrefab;

    void Start()
    {
        //Atatch the breaking script to the Animator, set it to play once, enable to play on collision
        gameObject.GetComponent<Animator>().enabled = false;
    }

    public void Break()
    {
        //Play animation of breaking
        gameObject.GetComponent<Animator>().enabled = true;
        //Make the player be able to pass through the statue
        gameObject.GetComponent<Collider2D>().enabled = false;

        //Spawn enemy if this statue contains one
        if (isEnemy)
        {
            Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }
}
