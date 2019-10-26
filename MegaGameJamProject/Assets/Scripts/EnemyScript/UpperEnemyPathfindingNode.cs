using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperEnemyPathfindingNode : MonoBehaviour
{

    public LayerMask pitfallMask;
    

    void Update() {

        

    }

    void OnTriggerStay2D(Collider2D collider) {
        UpperEnemyPathfinding pathfinding = collider.GetComponent<UpperEnemyPathfinding>();

        if(pathfinding != null) {

            Debug.Log("Pathfinder found!");

            //RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, pathfinding.transform.position - transform.position, 15f, pitfallMask.value);
            RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Player.INSTANCE.transform.position - transform.position, 15f, pitfallMask.value);

            if(/*enemyHit.collider != null && */playerHit.collider != null && playerHit.collider.CompareTag("Player")) {

                Debug.Log("Pathfinder located player!");

                pathfinding.SetTarget(transform.position);
            }

        }

    }

}
