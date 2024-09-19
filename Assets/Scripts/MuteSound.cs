using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    private Sprite soundOnImageMusic;
    public Sprite soundOffImageMusic;
    private Sprite soundOnImageSFX;
    public Sprite soundOffImageSFX;
    public Button buttonMusic;
    public Button buttonSFX;
    private bool isOn1 = true;
    private bool isOn2 = true;

    public AudioSource bgmSource;
    public AudioSource SFXSource;


    void Start()
    {
        soundOnImageMusic = buttonMusic.image.sprite;
        soundOffImageSFX = buttonSFX.image.sprite;
    }


    public void MusicButtonClicked()
    {
        if (isOn1)
        {
            buttonMusic.image.sprite = soundOffImageMusic;
            isOn1 = false;
            bgmSource.mute = true;
        }
        else
        {
            buttonMusic.image.sprite = soundOnImageMusic;
            isOn1 = true;
            bgmSource.mute = false;
        }
    }
    public void SFXButtonClicked()
    {
        if (isOn2)
        {
            buttonSFX.image.sprite = soundOffImageSFX;
            isOn2 = false;
            SFXSource.mute = true;
        }
        else
        {
            buttonSFX.image.sprite = soundOnImageSFX;
            isOn2 = true;
            SFXSource.mute = false;
        }
    }

}

