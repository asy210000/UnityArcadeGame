using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    public GameObject entryPrefab; // Assign your entry prefab in the inspector
    public Transform contentPanel; // Assign the ScrollView's Content Panel in the inspector

    // Call this method with leaderboard data to populate the panel
    public void PopulateLeaderboard(ScoreEntry[] leaderboardData)
    {
        // Clear old leaderboard entries
        foreach (Transform child in contentPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Add new leaderboard entries
        if (leaderboardData != null)
        {
            foreach (var entryData in leaderboardData)
            {
                GameObject entryObj = Instantiate(entryPrefab, contentPanel);
                LeaderboardEntry entry = entryObj.GetComponent<LeaderboardEntry>();
                if (entry != null)
                {
                    entry.SetData(entryData.username, entryData.score);
                }
            }
        }
        else
        {
            Debug.LogError("leaderboardData is null");
        }
    }

}
