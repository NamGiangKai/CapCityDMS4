using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    }

    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
            onScoreUpdated.Invoke(currentScore); // Notify listeners of the score update

            // Trigger background transition based on the score
            if (Mathf.FloorToInt(currentScore) > 0 && Mathf.FloorToInt(currentScore) % 10 == 0)
            {
                scrollingBackground.TransitionToNextBackground();
            }
        }
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0;
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
        onGameOver.Invoke();
    }

    public void ReturnToMainMenu()
    {
        isPlaying = false;
        onMainMenu.Invoke();
        // Additional logic to handle transitioning to the main menu, such as resetting the game state
    }

    public string PrettyScore()
    {
        return Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighscore()
    {
        return Mathf.RoundToInt(data.highscore).ToString();
    }
}
