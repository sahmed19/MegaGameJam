using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioLowPassFilter lowPassFilter;

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
}
