using UnityEngine;
using TMPro;

public class MainMenuCoinDisplay : MonoBehaviour
{
    public TMP_Text totalCoinsText;  // Reference to the TextMeshPro component

    void Start()
    {
        UpdateCoinDisplay();
    }

    void UpdateCoinDisplay()
    {
        int totalCoins = PlayerPrefs.GetInt("CoinCount", 0);  // Retrieve the saved coin count
        totalCoinsText.text = "Total Coins: " + totalCoins.ToString();  // Update the text UI
    }
}
