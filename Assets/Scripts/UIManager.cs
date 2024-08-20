using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameOverADUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighscoreUI;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private AudioSource soundEffects;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverADUI);
        UpdateMuteButtons();
    }

    public void PlayButtonHandler()
    {
        gm.StartGame();
    }

    public void ActivateGameOverADUI()
    {
        gameOverADUI.SetActive(true);
    }

    public void CloseAdButtonHandler()
    {
        gameOverADUI.SetActive(false);
        ActivateGameOverUI();
    }

    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.PrettyScore();
        gameOverHighscoreUI.text = "Highscore: " + gm.PrettyHighscore();
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

public void MusicMuteButtonHandler()
    {
        musicSource.mute = true;
        UpdateMusicMuteButtons();
    }

    public void MusicUnmuteButtonHandler()
    {
        musicSource.mute = false;
        UpdateMusicMuteButtons();
    }

    public void SFXMuteButtonHandler()
    {
        SFXSource.mute = true;
        UpdateSFXMuteButtons();
    }

    public void SFXUnmuteButtonHandler()
    {
        SFXSource.mute = false;
        UpdateMuteButtons();
    }

    private void UpdateMusicMuteButtons()
    {
        if (musicSource.mute)
        {
            muteButtonUI.SetActive(false);
            unmuteButtonUI.SetActive(true);
        }
        else
        {
            muteButtonUI.SetActive(true);
            unmuteButtonUI.SetActive(false);
        }
    }

    private void UpdateSFXMuteButtons()
    {
        if (musicSource.mute)
        {
            muteButtonUI.SetActive(false);
            unmuteButtonUI.SetActive(true);
        }
        else
        {
            muteButtonUI.SetActive(true);
            unmuteButtonUI.SetActive(false);
        }
    }
}
