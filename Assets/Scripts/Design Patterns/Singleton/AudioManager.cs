using Chapter.Singleton;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public Sound[] sounds;
    private Sound _currentMusic;

    public override void Awake()
    {
        base.Awake();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }

        if (_currentMusic != null && _currentMusic == s && _currentMusic.source.isPlaying)
        {
            return;
        }

        if (_currentMusic != null)
        {
            _currentMusic.source.Stop();
        }

        _currentMusic = s;
        _currentMusic.source.Play();
    }

    public void StopMusic()
    {
        if (_currentMusic != null)
        {
            _currentMusic.source.Stop();
        }
    }
   
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
