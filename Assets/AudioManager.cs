
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool isPlaying = false;
    
    void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    private void Start()
    {
        //theme which will be playing in backgorund
    }

    public void Play(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        if (!isPlaying)
        {
            s.source.Play();
            isPlaying = true;
        }
    }
    
    public void Stop(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
        isPlaying = false;
    }
}
