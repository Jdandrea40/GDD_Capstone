using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum Sounds
    {
        // PLAYER_WALK1,
        GUN_SHOT,
        BKG_LOOP,

        //as sounds are added to the project, add an enum for each sound
    }

    //dictionary for AudioClips to play
    private Dictionary<Sounds, AudioClip> soundDictionary = new Dictionary<Sounds, AudioClip>();

    //Adds Sounds to the dictionary
    private void AddSoundsToDictionary()
    {
        //soundDictionary.Add(Sounds.PLAYER_WALK1, Resources.Load<AudioClip>("Sounds/Walking"));
        soundDictionary.Add(Sounds.GUN_SHOT, Resources.Load<AudioClip>("Sounds/SFX_gunshot"));
        soundDictionary.Add(Sounds.BKG_LOOP, Resources.Load<AudioClip>("Sounds/SFX_backgroundLoop"));

        //add each sound to the dictionary here
    }

    //audio player components
    private AudioSource SFXSource;
    private AudioSource MusicSource;
    private AudioSource LoopSource;
    private List<AudioSource> Loops = new List<AudioSource>();

    private void Awake()
    {
        AddSoundsToDictionary();
        SFXSource = gameObject.AddComponent<AudioSource>();
        MusicSource = gameObject.AddComponent<AudioSource>();
    }

    //play a single sound effect through SFXSource
    public void PlaySFX(Sounds sound)
    {
        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(soundDictionary[sound]);
        }
    }

    //play a music clip through MusicSource
    public void PlayMusic(Sounds sound, bool loop)
    {
        MusicSource.clip = soundDictionary[sound];
        MusicSource.loop = loop;
        MusicSource.Play();
    }

    // Play sounds; dynamically instantiate loop Audio Sources as needed
    public void PlayLoop(Sounds sound)
    {
        LoopSource = gameObject.AddComponent<AudioSource>();
        LoopSource.clip = soundDictionary[sound];
        LoopSource.loop = true;
        LoopSource.Play();
        Loops.Add(LoopSource);
    }

    // Stop Loops
    public void StopLoop(Sounds sound)
    {
        if (Loops.Count > 0)
        {
            foreach (AudioSource loop in Loops)
            {
                Debug.Log("Loop.Clip: " + loop.clip + " : ParsedName: " + soundDictionary[sound]);
                if (loop.clip == soundDictionary[sound])
                {
                    Loops.Remove(loop);
                    Destroy(loop);
                    break;
                }
            }
        }
    }

    //Stop currently playing music
    public void StopMusic()
    {
        MusicSource.Stop();
        MusicSource.clip = null;
    }

    //Stop currently playing sound effect
    public void StopSFX()
    {
        SFXSource.Stop();
        SFXSource.clip = null;
    }

    //Stop all sound
    public void StopAllSound()
    {
        MusicSource.Stop();
        MusicSource.clip = null;

        SFXSource.Stop();
        SFXSource.clip = null;

        foreach (AudioSource loop in Loops)
        {
            Loops.Remove(loop);
            Destroy(loop);
        }
    }

    //set the volume of sound effects
    //parameter amount should be between 0 and 1
    public void SetSFXVolume(float amount)
    {
        SFXSource.volume = amount;
        LoopSource.volume = amount;
    }

    //set the volume of music
    //parameter amount should be between 0 and 1
    public void SetMusicVolume(float amount)
    {
        MusicSource.volume = amount;
    }
}