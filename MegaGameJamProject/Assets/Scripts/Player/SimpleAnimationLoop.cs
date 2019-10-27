using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationLoop : MonoBehaviour
{
    public Sprite[] sprites;

    SpriteRenderer renderer;

    public float timePerFrameInSeconds = .1f;

    float timer = 0f;

    public int frame = 0;

    void Start() {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {

        timer += Time.deltaTime;

        if(timer > timePerFrameInSeconds) {
            frame++;
            timer = 0;

            

            if(frame > sprites.Length - 1) {
                frame = 0;
            }

            renderer.sprite = sprites[frame];

            }

    }

}
