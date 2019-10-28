using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseHealer : MonoBehaviour
{
    
    float maxTime = 2.0f;
    float time = 0f;

    bool playerInZone = false;

    SpriteRenderer renderer;
    AudioSource source;
    public ParticleSystem particleSystem;
    public SpriteRenderer parentSpriteRenderer;

    void Start() {
        time = maxTime;
        renderer = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collider) {

        if(collider.CompareTag("Player")) {
            playerInZone = true;
        }

    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.CompareTag("Player")) {
            playerInZone = false;
        }
    }

    void FixedUpdate() {

        if(Player.INSTANCE.currentHealth < 4 && playerInZone) {
            
            time -= Time.fixedDeltaTime;
            

        } else {
            time += Time.fixedDeltaTime;
        }

        //time = Mathf.Clamp(time, 0f, maxTime);

        if (time > maxTime) {time = maxTime;}


        source.volume = Mathf.Lerp(0.0f, 0.5f, 1f - (time/maxTime));

        Debug.Log(source.volume);

        transform.localPosition = Vector3.up * (maxTime-time) * .1f + 
        (new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * .1f * (1f-(time/maxTime)));

        if(time <= 0.1f) {
            parentSpriteRenderer.color = new Color(1f, .5f, .5f, .6f);
            SoundFXManager.instance.PlaySound("FX", "Thunder", false, .8f);
            particleSystem.Emit(20);
            Player.INSTANCE.currentHealth++;
            Destroy(gameObject);
        }

    }



}
