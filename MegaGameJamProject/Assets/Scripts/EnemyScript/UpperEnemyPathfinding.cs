using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperEnemyPathfinding : MonoBehaviour
{
    
    Vector3 targetPosition;
    
    public LayerMask pitfallMask;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    void Update() {

        Debug.DrawLine(transform.position, targetPosition);

        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Player.INSTANCE.transform.position - transform.position, 5f, pitfallMask.value);
        if(playerHit.collider != null && playerHit.collider.CompareTag("Player")) {
            targetPosition = Player.INSTANCE.transform.position;
        }

        if((targetPosition-transform.position).sqrMagnitude > .2f) {
            transform.position += (targetPosition-transform.position).normalized * Time.deltaTime;
            animator.SetFloat("Speed", 1f);
        } else {
            animator.SetFloat("Speed", 0f);
        }
    }

    public void SetTarget(Vector3 _targetPosition) {
        targetPosition = _targetPosition;
    }

}
