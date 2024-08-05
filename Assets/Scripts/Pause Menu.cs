using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Time.timeScale = 0;
    }
}