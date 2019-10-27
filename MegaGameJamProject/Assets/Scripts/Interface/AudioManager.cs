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

    //Preserves Audio Source
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        lowPassFilter = GetComponent<AudioLowPassFilter>();
    }

    private void Update()
    {
        if(Player.INSTANCE != null)
        {
            lowPassFilter.cutoffFrequency = Player.INSTANCE.PlayerInUnderworld() ? 1000 : 5000;
        }
    }

    public void StartGameAudio()
    {
        gameStarted = true;
    }
}
