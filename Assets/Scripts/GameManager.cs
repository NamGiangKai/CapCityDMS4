using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public float currentScore = 0f;
    public Data data;
    public ScrollingBackground scrollingBackground; // Reference to the ScrollingBackground script

    public bool isPlaying = false;
    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();
    public UnityEvent onMainMenu = new UnityEvent(); // Event for returning to the main menu
    public UnityEvent<float> onScoreUpdated = new UnityEvent<float>(); // Score update event
    public UnityEvent onPlayerDie = new UnityEvent(); // Event triggered when the player dies
    public UnityEvent onResume = new UnityEvent(); // Event for resuming the game

    private void Start()
    {
        string loadedData = SaveSystem.Load("save");
        if (loadedData != null)
        {
            data = JsonUtility.FromJson<Data>(loadedData);
        }
        else
        {
            data = new Data();
        }

        // Initialize MissionManager if not already in the scene
        if (MissionManager.instance == null)
        {
            GameObject missionManagerObject = new GameObject("MissionManager");
            missionManagerObject.AddComponent<MissionManager>();
        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            // Handle any real-time game updates here if needed
        }
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        Time.timeScale = 1; // Ensure the game is running
        currentScore = 0; // Reset the score for this session
    }

    public void GameOver()
    {
        if (data.highscore < currentScore)
        {
            data.highscore = currentScore;
            string saveString = JsonUtility.ToJson(data);
            SaveSystem.Save("save", saveString);
        }

        isPlaying = false;
        Time.timeScale = 0; // Pause the game
        MissionManager.instance.CheckMissions(); // Check missions upon game over
        onGameOver.Invoke();
    }

    public void ReturnToMainMenu()
    {
        isPlaying = false;
        MissionManager.instance.CheckMissions(); // Check missions before returning to the main menu
        onMainMenu.Invoke();
        // Additional logic to handle transitioning to the main menu, such as resetting the game state
        SceneManager.LoadScene("MainMenu"); // Make sure to load the main menu scene
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighscore()
    {
        return Mathf.RoundToInt(data.highscore).ToString();
    }

    public void PlayerJumpedOverObstacle()
    {
        currentScore += 1;
        onScoreUpdated.Invoke(currentScore); // Notify listeners of the score update

        // Update mission progress
        MissionManager.instance.UpdateMissionProgress("Score", 1);

        // Trigger background transition based on the score
        if (Mathf.FloorToInt(currentScore) % 10 == 0)
        {
            scrollingBackground.TransitionToNextBackground();
        }
    }

    public void CollectCoin(int coins)
    {
        MissionManager.instance.UpdateMissionProgress("Collect", coins);
    }

    public void PlayerDied()
    {
        onPlayerDie.Invoke(); // Trigger the onPlayerDie event

        // Handle any additional game over logic here
        GameOver();
    }

    // Updated method to resume the game after closing the ad
 public void ResumeGame()
{
    isPlaying = true;
    Time.timeScale = 1; // Resume the game

    // Resume background scrolling
    if (scrollingBackground != null)
    {
        scrollingBackground.ResumeBackgroundScrolling(); // Call to resume scrolling
    }

    // Invoke the resume event to notify other systems that the game has resumed
    onResume.Invoke();
}

}
