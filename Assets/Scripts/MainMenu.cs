using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     [SerializeField] GameObject settingMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void SettingMainMenu()
    {
        settingMenu.SetActive(true);
    }

    public void BackMainMenu()
    {
        settingMenu.SetActive(false);
    }

    public void ShopScene()
    {
        SceneManager.LoadScene("Shop");
    }
}
