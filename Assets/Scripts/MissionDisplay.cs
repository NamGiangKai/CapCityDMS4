using UnityEngine;
using TMPro;

public class MissionDisplay : MonoBehaviour
{
    public TMP_Text missionText; // Reference to the TextMeshPro component

    void Update()
    {
        // Clear the mission text at the start of each update
        missionText.text = "";

        // Loop through all active missions and display them
        foreach (Mission mission in MissionManager.instance.missions)
        {
            missionText.text += mission.description + ": " + mission.currentProgress + "/" + mission.goal + "\n";
        }
    }
}
