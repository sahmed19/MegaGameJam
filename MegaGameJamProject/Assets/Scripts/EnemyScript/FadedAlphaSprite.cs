using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadedAlphaSprite : MonoBehaviour
{
    
    SpriteRenderer renderer;

    public Color color;

    public float speed = 1.0f;
    [Range(0f, 1f)]
    public float lowerBound = 0.0f;
    [Range(0f, 1f)]
    public float higherBound = 1.0f;

    void Start() {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        
        Color c = color;

        c.a = Mathf.Lerp(lowerBound, higherBound, .5f + Mathf.Sin(speed * Time.time + gameObject.GetInstanceID())/2f);

        renderer.color = c;

    }

}
