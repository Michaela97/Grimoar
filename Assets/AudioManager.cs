using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("Skellig");
    }

    public void Play(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);

        try
        {
            if (!s.IsPlaying)
            {
                s.source.Play();
                s.IsPlaying = true;
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log("There is no audio with name:" + name);
        }
    }

    public void Stop(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);

        try
        {
            if (s.IsPlaying)
            {
                s.source.Stop();
                s.IsPlaying = false;
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log("There is no audio with name:" + name);
        }
    }

    public void PlayOnce(string name)
    {
        var s = Array.Find(sounds, sound => sound.name == name);

        try
        {
            s.source.Play();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("There is no audio with name:" + name);
        }
    }
}