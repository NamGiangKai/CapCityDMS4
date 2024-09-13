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
    [SerializeField] private GameObject adVideoUI; // New GameObject for ad video UI
    [SerializeField] private GameObject adCloseButton; // New GameObject for close button
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighscoreUI;

    private GameManager gm;
    public TMP_Text coinText; // Assign this in the Inspector
    private CoinSaveSystem saveSystem;

    private void Start()
    {
        gm = GameManager.Instance;

        if (gm == null)
        {
            Debug.LogError("GameManager instance is not found. Make sure there is a GameManager in the scene.");
            return;
        }

        gm.onGameOver.AddListener(ActivateGameOverADUI);
        saveSystem = FindObjectOfType<CoinSaveSystem>();

        if (saveSystem == null)
        {
            Debug.LogError("CoinSaveSystem instance is not found. Make sure there is a CoinSaveSystem in the scene.");
            return;
        }

        ShowPlayerCoins();
    }

    private void OnGUI()
    {
        if (scoreUI == null || gm == null)
        {
            Debug.LogError("UIManager: scoreUI or GameManager instance is not assigned.");
            return;
        }

        scoreUI.text = gm.PrettyScore();
    }

    void ShowPlayerCoins()
    {
        if (saveSystem == null)
        {
            Debug.LogError("SaveSystem is not assigned.");
            return;
        }

        Data playthroughData = saveSystem.GetData();
        coinText.text = "Playthrough Coin History:\n";
        for (int i = 0; i < playthroughData.coinHistory.Count; i++)
        {
            coinText.text += $"Playthrough {i + 1}: {playthroughData.coinHistory[i]} coins\n";
        }
    }

    public void PlayButtonHandler()
    {
        if (gm == null)
        {
            Debug.LogError("GameManager instance is not assigned.");
            return;
        }

        gm.StartGame();
    }

    public void ActivateGameOverADUI()
    {
        if (gameOverADUI == null)
        {
            Debug.LogError("GameOverADUI is not assigned.");
            return;
        }

        gameOverADUI.SetActive(true);
    }

    // New method to show ad video and close button
    private void ShowAdVideo()
    {
        if (adVideoUI == null || adCloseButton == null)
        {
            Debug.LogError("Ad video UI or close button is not assigned.");
            return;
        }

        adVideoUI.SetActive(true); // Show ad video UI
        adCloseButton.SetActive(true); // Show close button
    }

    // Method to handle closing the ad video
    public void CloseAdButtonHandler()
    {
        if (adVideoUI == null || adCloseButton == null)
        {
            Debug.LogError("Ad video UI or close button is not assigned.");
            return;
        }

        adVideoUI.SetActive(false); // Hide ad video UI
        adCloseButton.SetActive(false); // Hide close button
        gameOverADUI.SetActive(false); // Hide game over ad UI
        gm.ResumeGame(); // Resume the game without triggering game over
    }

    public void CloseAdButtonHandler1()
    {
        gameOverADUI.SetActive(false); // Hide game over ad UI
        ActivateGameOverUI();
        Time.timeScale = 1;
    }

    public void ActivateGameOverUI()
    {
        if (gameOverUI == null || gameOverScoreUI == null || gameOverHighscoreUI == null)
        {
            Debug.LogError("GameOverUI, GameOverScoreUI, or GameOverHighscoreUI is not assigned.");
            return; 
        }

        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.PrettyScore();
        gameOverHighscoreUI.text = "Highscore: " + gm.PrettyHighscore();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
