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
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverADUI);
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
}
