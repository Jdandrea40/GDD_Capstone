using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public enum Sounds
    {
        // PLAYER_WALK1,
        GUN_SHOT,
        CRYO_EXPLODE1,
        CRYO_EXPLODE2,
        INCEND_EXPLODE1,
        NORM_EXPLODE,
        MISSLE_LAUNCH,
        BKG_LOOP,
        ITEM_PICKUP,
        TOGGLE_CLICK,
        PLACE_TOWER,
        BUTTON_CLICK

        //as sounds are added to the project, add an enum for each sound
    }

    //dictionary for AudioClips to play
    private Dictionary<Sounds, AudioClip> soundDictionary = new Dictionary<Sounds, AudioClip>();

    //Adds Sounds to the dictionary
    private void AddSoundsToDictionary()
    {
        #region PROJECTILES
        soundDictionary.Add(Sounds.GUN_SHOT, Resources.Load<AudioClip>("Sounds/Projectile/SFX_gunshot"));
        soundDictionary.Add(Sounds.CRYO_EXPLODE1, Resources.Load<AudioClip>("Sounds/Projectile/SFX_cryoExplode1.2"));
        soundDictionary.Add(Sounds.CRYO_EXPLODE2, Resources.Load<AudioClip>("Sounds/Projectile/SFX_cryoExplode2"));
        soundDictionary.Add(Sounds.INCEND_EXPLODE1, Resources.Load<AudioClip>("Sounds/Projectile/SFX_incendiaryExplode"));
        soundDictionary.Add(Sounds.NORM_EXPLODE, Resources.Load<AudioClip>("Sounds/Projectile/SFX_normalExplode1"));
        soundDictionary.Add(Sounds.MISSLE_LAUNCH, Resources.Load<AudioClip>("Sounds/Projectile/SFX_missleLaunch1"));
        #endregion

        soundDictionary.Add(Sounds.BKG_LOOP, Resources.Load<AudioClip>("Sounds/SFX_bkgLoop1"));
        soundDictionary.Add(Sounds.ITEM_PICKUP, Resources.Load<AudioClip>("Sounds/SFX_itemPickUp"));

        #region UI
        soundDictionary.Add(Sounds.PLACE_TOWER, Resources.Load<AudioClip>("Sounds/UI/SFX_placeTower"));
        soundDictionary.Add(Sounds.TOGGLE_CLICK, Resources.Load<AudioClip>("Sounds/UI/SFX_toggleClick"));
        soundDictionary.Add(Sounds.BUTTON_CLICK, Resources.Load<AudioClip>("Sounds/UI/SFX_toggleClick"));

        #endregion




        //add each sound to the dictionary here
    }

    //audio player components
    private AudioSource SFXSource;
    public AudioSource MusicSource;
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
        //if (!SFXSource.isPlaying)
        //{
            SFXSource.PlayOneShot(soundDictionary[sound]);
        //}
    }
    public void PlayButtonClick(Sounds sound)
    {
        SFXSource.clip = soundDictionary[sound];
        SFXSource.loop = false;
        SFXSource.Play();

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