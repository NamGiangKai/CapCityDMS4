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
    public AudioClip checkpoint;
    public AudioClip wallTouch;
    public AudioClip portalIn;
    public AudioClip portalOut;

    [Header("--------- Music Playlist ---------")]
    public List<AudioClip> musicPlaylist = new List<AudioClip>();
    private int currentTrackIndex = 0;

    void Start()
    {
        if (musicPlaylist.Count > 0)
        {
            PlayNextTrack();
        }
    }

    void Update()
    {
        if (!musicSource.isPlaying && musicPlaylist.Count > 0)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        musicSource.clip = musicPlaylist[currentTrackIndex];
        musicSource.Play();
        currentTrackIndex = (currentTrackIndex + 1) % musicPlaylist.Count;
    }

    public void AddMusicTrack(AudioClip newTrack)
    {
        musicPlaylist.Add(newTrack);
    }

    public void RemoveMusicTrack(AudioClip trackToRemove)
    {
        if (musicPlaylist.Contains(trackToRemove))
        {
            musicPlaylist.Remove(trackToRemove);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
