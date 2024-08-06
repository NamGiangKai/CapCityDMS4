using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include this namespace for scene management

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Setting()
    {
        settingMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Back()
    {
        settingMenu.SetActive(false);
        pauseMenu.SetActive(true); // Return to pause menu with time still paused
    }

    public void MainMenu()
    {
        Time.timeScale = 1; // Resume time before loading the main menu
        SceneManager.LoadScene("PlayScene"); // Load the main menu scene
    }
}
