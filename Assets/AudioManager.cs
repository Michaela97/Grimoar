
using System;
using Player;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private int i = 0;
    
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
        Play("Skellig");
    }

    public void Play(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        
        if (!s.IsPlaying)
        {
            s.source.Play();
            s.IsPlaying= true;
            i++;
        }
    }
    
    public void Stop(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);
        if (s.IsPlaying)
        {
            s.source.Stop();
            s.IsPlaying = false;
        }
       
    }
}
