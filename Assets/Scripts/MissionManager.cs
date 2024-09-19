using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    public List<Mission> missions; // List of active missions

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        missions = new List<Mission>();
        IssueNewMission(); // Start with an initial mission
    }

    public void IssueNewMission()
    {
        // Example missions; expand this with more logic or randomization
        missions.Add(new Mission("Score 100 points", 100));
        missions.Add(new Mission("Collect 10 coins", 10));

    }

    public void CheckMissions()
    {
        for (int i = missions.Count - 1; i >= 0; i--)
        {
            if (missions[i].isCompleted)
            {
                missions.RemoveAt(i);
                IssueNewMission();
            }
        }
    }

    public void UpdateMissionProgress(string missionType, int amount)
    {
        foreach (Mission mission in missions)
        {
            if (!mission.isCompleted && mission.description.Contains(missionType))
            {
                mission.AddProgress(amount);
                if (mission.isCompleted)
                {
                    // Optional: logic when a mission is completed
                    // Could be to notify the player, move to the next mission, etc.
                }
                break;
            }
        }
        CheckMissions();
    }
}
