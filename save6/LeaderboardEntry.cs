using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace

public class LeaderboardEntry : MonoBehaviour
{
    public TMP_Text usernameText; // Use TMP_Text instead of Text
    public TMP_Text scoreText; // Use TMP_Text instead of Text

    public void SetData(string username, int score)
    {
        if (usernameText != null)
            usernameText.text = username;

        if (scoreText != null)
            scoreText.text = score.ToString();
    }
}
