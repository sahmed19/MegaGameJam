using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioLowPassFilter lowPassFilter;
    [SerializeField] AudioClip nonPersistentUpper;
    [SerializeField] AudioClip nonPersistentAbyss;
    public bool gameStarted;
    AudioSource audioNonPersistent;

    //Preserves Audio Source
    private void Awake()
    {
    }

    private void Start()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        audioNonPersistent = gameObject.GetComponent<AudioSource>();
    }
        
    

    public void StartGameAudio()
    {
        gameStarted = true;
    }

    void PlayNonPersistent(AudioClip clip)
    {
        audioNonPersistent.PlayOneShot(clip, 0.7f);
    }
}
