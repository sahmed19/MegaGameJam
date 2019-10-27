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
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
        audioNonPersistent = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameStarted)
        {
            
            if (Player.INSTANCE != null)
            {
                lowPassFilter.cutoffFrequency = Player.INSTANCE.PlayerInUnderworld() ? 2000 : 5000;
                audioNonPersistent.volume = Player.INSTANCE.PlayerInUnderworld() ? .3f : .5f;
            }
        }
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
