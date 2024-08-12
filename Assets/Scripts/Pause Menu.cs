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
    public void SettingMainMenu()
    {
        settingMenu.SetActive(true);
    }

    public void BackMainMenu()
    {
        settingMenu.SetActive(false);
    }

    public void InGameBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}

    

