using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;
    public int score;
    public TMP_Text scoreText; // Reference to the score text UI component

    // Set these properties with the appropriate values
    public int playerID; // Should be set when the player logs in
    public string gameName; // The name of your game
    public int playTime; // The play time to be sent along with the score

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This object persists across scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load events
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        UpdateScoreDisplay();
    }

    public void UpdateScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }

    // Call this method when the game ends to submit the score
    public void SubmitScore(int playerID, string gameName, int score, int playTime)
    {
        // Log the score
        Debug.Log($"Submitting score: {score} for playerID: {playerID}, gameName: {gameName}, playTime: {playTime}");

        // Assuming that Main.Instance.Web.SubmitScore() is correctly implemented
        StartCoroutine(Main.Instance.Web.SubmitScore(playerID, gameName, score, playTime, (response) =>
        {
            Debug.Log($"Server response: {response}");
        }));
    }

}
