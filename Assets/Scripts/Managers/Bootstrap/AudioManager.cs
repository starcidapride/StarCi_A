using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonPersistent<AudioManager>
{
    public AudioSource audioSource;

    [SerializeField]
    private AudioClip music;

    public void Start()
    { 
        audioSource.clip = music;

        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SetMute(bool isMute)
    {
        audioSource.mute = isMute;
    }

}
