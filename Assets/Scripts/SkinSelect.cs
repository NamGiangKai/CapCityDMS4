using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelect : MonoBehaviour
{
    public GameObject[] playerPrefabs; 
    int characterIndex;

    private void Awake()
    {
        // Use PlayerPrefs to get the saved character index
        characterIndex = PlayerPrefs.GetInt("CharacterSkin", 0);

        // Instantiate the selected player prefab at position (0, 0, 0) with no rotation
        Instantiate(playerPrefabs[characterIndex], Vector3.zero, Quaternion.identity);
    }

}
