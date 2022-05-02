using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;
    public AudioSource VFXSource;

    // Random pitch adjustment range.
    public float LowPitchRange = .95f;
    public float HighPitchRange = 1.05f;

    // Singleton instance.
    public static SoundManager Instance = null;

    //Audio Clips
    public AudioClip dart_Hit;
    public AudioClip dart_Throw;
    public AudioClip dart_Destroy;
    public AudioClip dart_Reload;
    public AudioClip object_Placed;
    public AudioClip plane_Scanning;
    public AudioClip dart_GameBackMusic;
    public AudioClip dart_DoubleScoreSound;
    public AudioClip dart_TrippleScoreSound;
    public AudioClip power_selectSound;
    public AudioClip power_PerfectSelectSound;
    public AudioClip game_NewRecordMadeSound;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        play_dartbackMusic();
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        if (clip)
        {
            EffectsSource.clip = clip;
            EffectsSource.Play();
        }
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        if (clip)
        {
            MusicSource.clip = clip;
            MusicSource.Play();
        }
    }

    // Play a single clip through the music source.
    public void PlayVFX(AudioClip clip)
    {
        if (clip)
        {
            VFXSource.clip = clip;
            VFXSource.Play();
        }
    }

    // Play a random clip from an array, and randomize the pitch slightly.
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

        EffectsSource.pitch = randomPitch;
        EffectsSource.clip = clips[randomIndex];
        EffectsSource.Play();
    }

    // Helper Methods
    public void play_ObjectPlacedSound()
    {
        Play(object_Placed);
    }

    public void play_dartHitSound()
    {
        Play(dart_Hit);
    }

    public void play_dartThrowSound()
    {
        Play(dart_Throw);
    }

    public void play_dartDestroySound()
    {
        Play(dart_Destroy);
    }

    public void play_dartReloadSound()
    {
        Play(dart_Reload);
    }

    public void play_dartbackMusic()
    {
        //PlayMusic(dart_GameBackMusic);
    }

    public void play_DoubleScoreSound()
    {
        PlayVFX(dart_DoubleScoreSound);
    }
    public void play_TrippleScoreSound()
    {
        PlayVFX(dart_TrippleScoreSound);
    }

    public void play_PowerSelectSound()
    {
        PlayVFX(power_selectSound);
    }

    public void play_PowerPerfectSelectSound()
    {
        PlayVFX(power_PerfectSelectSound);
    }

    public void play_NewRecordMadeSound()
    {
        PlayVFX(game_NewRecordMadeSound);
    }

}
