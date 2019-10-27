using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioManager : MonoBehaviour
{

    AudioLowPassFilter lowPassFilter;
    [SerializeField] AudioClip persistentUpper;
    [SerializeField] AudioClip nonPersistentUpper;
    [SerializeField] AudioClip persistentAbyss;
    [SerializeField] AudioClip nonPersistentAbyss;
    public bool gameStarted;
    AudioSource audioNonPersistent;
    AudioSource audioPersistent;

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
        if (Player.INSTANCE != null)
        {
            lowPassFilter.cutoffFrequency = Player.INSTANCE.PlayerInUnderworld() ? 1000 : 5000;

            if (gameStarted)
            {
                if (!Player.INSTANCE.PlayerInUnderworld())
                {
                    audioPersistent = new AudioSource();
                    audioPersistent.clip = nonPersistentUpper;
                    audioPersistent.volume = 0f;
                    FadeIn();
                }

                else
                {
 
                }
            }
        }
    }

    public void StartGameAudio()
    {
        gameStarted = true;
    }

    void PlayOnce(AudioClip clip)
    {
        audioNonPersistent.PlayOneShot(clip, 0f);
    }

    void PlayPersistent(AudioClip clip)
    {

    }

    void FadeIn()
    {
        float fadeTime = 2f;

        while(audioPersistent.volume < 1f)
        {
            audioPersistent.volume += Time.deltaTime / fadeTime;
            Debug.Log(audioPersistent.volume);
        }
    }
}
