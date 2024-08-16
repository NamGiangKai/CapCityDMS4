using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------- Audio Clip ---------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip run;
    public AudioClip jump;
    public AudioClip button1;
    public AudioClip button2;
    public AudioClip button3;

    // [Header("--------- Music Playlist ---------")]
    // public List<AudioClip> musicPlaylist = new List<AudioClip>();
    // private int currentTrackIndex = 0;

    // void Start()
    // {
    //     if (musicPlaylist.Count > 0)
    //     {
    //         PlayNextTrack();
    //     }
    // }

    // void Update()
    // {
    //     if (!musicSource.isPlaying && musicPlaylist.Count > 0)
    //     {
    //         PlayNextTrack();
    //     }

    // }

    // void PlayNextTrack()
    // {
    //     musicSource.clip = musicPlaylist[currentTrackIndex];
    //     musicSource.Play();
    //     currentTrackIndex = (currentTrackIndex + 1) % musicPlaylist.Count;
    // }

    // public void AddMusicTrack(AudioClip newTrack)
    // {
    //     musicPlaylist.Add(newTrack);
    // }

    // public void RemoveMusicTrack(AudioClip trackToRemove)
    // {
    //     if (musicPlaylist.Contains(trackToRemove))
    //     {
    //         musicPlaylist.Remove(trackToRemove);
    //     }
    // }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = background;
        musicSource.loop = true;  // Enable looping
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // New Method to Play Run Sound
    public void PlayRunSound()
    {
        if (!SFXSource.isPlaying || SFXSource.clip != run)
        {
            SFXSource.clip = run;
            SFXSource.loop = true;
            SFXSource.Play();
        }
    }

    // New Method to Stop Run Sound
    public void StopRunSound()
    {
        if (SFXSource.isPlaying && SFXSource.clip == run)
        {
            SFXSource.Stop();
        }
    }

    // New Method to Play Jump Sound with adjustable speed (pitch)
    public void PlayJumpSound(float pitch=1f)
    {
        SFXSource.pitch = pitch;  // Adjust the pitch for the jump sound
        PlaySFX(jump);
        SFXSource.pitch = 1f;  // Reset pitch to default after playing the jump sound
        // //                        // Stop the running sound to ensure the jump sound plays
        StopRunSound();

        // // Adjust the pitch for the jump sound
        SFXSource.pitch = pitch;

        // Play the jump sound
        PlaySFX(jump);

        // Reset pitch to default after playing the jump sound
        SFXSource.pitch = 1f;
    }

    // New Method to Play Death Sound
    public void PlayDeathSound()
    {
        StopRunSound();  // Stop the running sound when the character dies
        PlaySFX(death);
    }

    public void PlayButton1Sound()
    {
        PlaySFX(button1);
    }

    public void PlayButton2Sound()
    {
        PlaySFX(button2);
    }

    public void PlayButton3Sound()
    {
        PlaySFX(button3);
    }
}
