using UnityEngine;

public class CoinSaveSystem : MonoBehaviour
{
    private const string FILE_NAME = "coinData"; // File name for the save data
    private Data  Data = new  Data();

    void Awake()
    {
        LoadCoins();
    }

    public void SaveCoins(int coins)
    {
         Data.coinHistory.Add(coins); // Add the current  's coins to the history
        string jsonData = JsonUtility.ToJson( Data, true);
        SaveSystem.Save(FILE_NAME, jsonData); // Save the data using SaveSystem
    }

    public void LoadCoins()
    {
        string jsonData = SaveSystem.Load(FILE_NAME);
        if (jsonData != null)
        {
             Data = JsonUtility.FromJson< Data>(jsonData);
        }
        else
        {
             Data = new  Data(); // Initialize with empty data if no save file exists
        }
    }

    public  Data GetData()
    {
        return  Data;
    }

    public void ClearSaveData()
    {
        SaveSystem.Save(FILE_NAME, ""); // Clear the save file by saving an empty string
         Data = new  Data(); // Clear in-memory data as well
    }
}
