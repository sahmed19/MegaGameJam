using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EntitySpriteLayerer : MonoBehaviour
{
    
    SpriteRenderer renderer;

    void Start() {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        renderer.sortingOrder = (int) (-transform.position.y * 10f);
    }

}
