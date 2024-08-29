using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter instance;

    public TMP_Text coinText;
    public int currentCoins = 0;

    void Awake()
    {
        instance = this;
        LoadCoins();  // Load the saved coin value when the game starts
    }

    void Start()
    {
        coinText.text = "COINS: " + currentCoins.ToString();
    }

    public void IncreaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = "COINS: " + currentCoins.ToString();
        SaveCoins();  // Save the updated coin value whenever it changes

        // Notify the MissionManager about the coin collection (if applicable)
        MissionManager.instance?.UpdateMissionProgress("Collect", v);
    }

    public void ResetCoins()
    {
        currentCoins = 0;
        coinText.text = "COINS: " + currentCoins.ToString();
        SaveCoins();  // Save the reset coin value
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("CoinCount", currentCoins);
        PlayerPrefs.Save();  // Save the PlayerPrefs to persist the data
    }

    public void LoadCoins()
    {
        if (PlayerPrefs.HasKey("CoinCount"))
        {
            currentCoins = PlayerPrefs.GetInt("CoinCount");
        }
        else
        {
            currentCoins = 0;  // Default to 0 if there's no saved data
        }
    }

    void OnApplicationQuit()
    {
        SaveCoins();  // Ensure coins are saved when the game is closed
    }
}
