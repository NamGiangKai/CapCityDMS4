using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     [SerializeField] GameObject settingMenu;
     [SerializeField] GameObject userNameMenu;
     [SerializeField] GameObject missionPopUpMenu;
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
        missionPopUpMenu.SetActive(false);
    }

    public void MissionPopUp()
    {
        missionPopUpMenu.SetActive(true);
    }


    public void ShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }

    // public void SkinScene()
    // {
    //     SceneManager.LoadScene("SkinScene");
    // }
}
