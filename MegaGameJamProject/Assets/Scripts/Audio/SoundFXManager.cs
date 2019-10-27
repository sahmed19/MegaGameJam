using UnityEngine;
using System;
using System.Collections.Generic;

  
using UnityEngine.Audio;
using UnityEngine;
using System;

[Serializable]
public class Sound
{
    public string Name;
    public AudioClip AudioClip;
    public AudioSource audioSource;
}

[Serializable]
/// <summary>
/// A group of sounds
/// </summary>
public class SoundGroup
{
    private SoundFXManager SoundFXManager;

    public string Name;

    public AudioSource audioSource;
    public AudioMixerGroup mixerGroup;

    [SerializeField]
    [Tooltip("Group of sounds")]
    public Sound[] sounds;



	//Collection of sounds for easy access
    public Dictionary<string, Sound> soundDictionary;

    public void Initialize(SoundFXManager SoundFXManager)
    {
        this.SoundFXManager = SoundFXManager;
        soundDictionary = new Dictionary<string, Sound>(sounds.Length);
        if (audioSource != null && mixerGroup != null)
            audioSource.outputAudioMixerGroup = mixerGroup;

        foreach (Sound sound in sounds)
        {
            if (audioSource != null)
                sound.audioSource = audioSource;
            else if (sound.audioSource == null)
            {
                sound.audioSource = SoundFXManager.gameObject.AddComponent<AudioSource>();
                sound.audioSource.outputAudioMixerGroup = mixerGroup;
                sound.audioSource.clip = sound.AudioClip;
            }
            if (!soundDictionary.ContainsKey(sound.Name))
                soundDictionary.Add(sound.Name, sound);
            else
                Debug.LogError("Error: You can not have two sounds with the same name.");
        }
    }

    /// <summary>
    /// Get the sound by name
    /// </summary>
    /// <param name="soundName">The name of the sound</param>
    /// <returns>Returns the sound if found</returns>
    public Sound GetSound(string soundName)
    {
        Sound sound;
        if (soundDictionary.TryGetValue(soundName, out sound))
        {
            return sound;
        }
        else
            Debug.LogError("Error: Sound could not be returned. Sound not found.");
        return null;
    }

	public Sound[] GetSounds() {
		return sounds;
	}

}

/// <summary>
/// Manager for all of the sound fx and music
/// </summary>
public class SoundFXManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the sound manager
    /// </summary>
    public static SoundFXManager instance;

    [Header("Audio")]
    [SerializeField]
    [Tooltip("Collection of sounds")]
    private SoundGroup[] soundGroups;

    private Dictionary<string, SoundGroup> soundGroupDictionary;

    private void Awake()
    {
        //Maintain singleton instance
        if (instance == null)
            instance = this;
        //Ensure there is only one instance of the SoundFXManager
        else if (instance != this)
            Destroy(gameObject);
        //Persist between scenes
        //DontDestroyOnLoad(gameObject);
        soundGroupDictionary = new Dictionary<string, SoundGroup>(soundGroups.Length);
        foreach (SoundGroup group in soundGroups)
        {
            soundGroupDictionary.Add(group.Name, group);
            group.Initialize(this);
        }
    }

    public void PlaySoundSimple(string soundAddress) {

        string[] soundAddressParsed = soundAddress.Split('/');

        string soundGroupName = soundAddressParsed[0];
        string soundName = soundAddressParsed[1];

        PlaySound(soundGroupName, soundName);

    }

    /// <summary>
    /// Play a sound
    /// </summary>
    /// <param name="soundName">The name of the sound</param>
    /// <param name="loop">Whether to loop the sound (optional)</param>
    /// <param name="volume">The volume of the sound (optional)</param>
    /// <param name="pitch">The pitch of the sound (optional)</param>
    public void PlaySound(string soundGroupName, string soundName, bool loop = false, float volume = 1, float pitch = 1)
    {
        SoundGroup group = null;
        if (soundGroupDictionary.TryGetValue(soundGroupName, out group))
        {
            Sound sound = group.GetSound(soundName);
            if (sound == null)
            {
                Debug.LogError("Error: Sound could not be played. Sound not found.");
                return;
            }

            sound.audioSource.clip = sound.AudioClip;
            sound.audioSource.loop = loop;
            sound.audioSource.volume = volume;
            sound.audioSource.pitch = pitch;
            sound.audioSource.Play();
        }
        else
            Debug.LogError("Error: Sound could not be played. SoundGroup not found.");
    }

    /// <summary>
    /// Play a sound
    /// </summary>
    /// <param name="soundName">The name of the sound</param>
    /// <param name="loop">Whether to loop the sound (optional)</param>
    /// <param name="volume">The volume of the sound (optional)</param>
    /// <param name="pitch">The pitch of the sound (optional)</param>
    public void PlaySound(string soundGroupName, int soundIndex, bool loop = false, float volume = 1, float pitch = 1)
    {
        SoundGroup group = null;
        if (soundGroupDictionary.TryGetValue(soundGroupName, out group))
        {
            Sound sound = group.GetSounds()[soundIndex];
            if (sound == null)
            {
                Debug.LogError("Error: Sound could not be played. Sound not found.");
                return;
            }
            sound.audioSource.loop = loop;
            sound.audioSource.volume = volume;
            sound.audioSource.pitch = pitch;
            sound.audioSource.Play();
        }
        else
            Debug.LogError("Error: Sound could not be played. SoundGroup not found.");
    }

    /// <summary>
    /// Gets the sound group
    /// </summary>
    /// <returns>The sound group</returns>
    /// <param name="soundGroupName">Sound group name</param>
    public SoundGroup GetSoundGroup(string soundGroupName)
    {
        SoundGroup group = null;
        if (!soundGroupDictionary.TryGetValue(soundGroupName, out group))
            Debug.LogError("Error: SoundGroup not found.");
        return group;
    }

    /// <summary>
    /// Stops a sound from playing
    /// </summary>
    /// <param name="soundName">The name of the sound</param>
    public void StopSound(string soundGroupName, string soundName)
    {
        SoundGroup group = null;
        if (soundGroupDictionary.TryGetValue(soundGroupName, out group))
        {
            Sound sound = group.GetSound(soundName);
            if (sound == null)
            {
                Debug.LogError("Error: Sound could not be stopped. Sound not found.");
                return;
            }
            sound.audioSource.Stop();
        }
        else
            Debug.LogError("Error: Sound could not be stopped. SoundGroup not found.");
    }


}