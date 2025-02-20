using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverPanel; // Reference to the game over panel UI component
    public GameObject leaderboardPanel;
    private float gameStartTime; // To track when the game starts for play time calculation
    //[HideInInspector]
    [System.NonSerialized] public int playerID = 10; // The player's ID (you should replace this with actual login logic)
    public string gameName = "Cosmic Invaders"; // The name of your game

    void Awake()
    {
        Debug.Log($"GameManager Awake called on instance {GetInstanceID()} in scene {SceneManager.GetActiveScene().name}");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameStartTime = Time.time; // Set the start time here
        }
        else if (instance != this)
        {
            Debug.LogWarning($"Another instance of GameManager was found on instance {GetInstanceID()} and destroyed.");
            Destroy(gameObject);
        }
    }


    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            Debug.Log("GameManager: Showing Game Over panel.");
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("GameManager: Game Over panel is not assigned.");
        }
    }

    public void GameOver()
    {
        Debug.Log("GameManager: Game Over called.");
        Time.timeScale = 0;
        ShowGameOver(); // Call ShowGameOver to display the panel

        // Calculate the play time based on the time elapsed since the game started
        float playTime = Time.time - gameStartTime;

        if (Web.instance != null)
        {
            Web.instance.ShowBestScores(gameName);
        }

        if (MusicManager.instance != null)
        {
            MusicManager.instance.StopMusic();
        }

        // Check that the PointManager instance is not null before trying to use it
        if (PointManager.instance != null)
        {
            // Submit the score using the PointManager
            // The playTime should be an integer number of seconds, so cast the float to int
            PointManager.instance.SubmitScore(gameName, PointManager.instance.score, (int)playTime);
        }
        else
        {
            Debug.LogError("PointManager instance is not set.");
        }
    }
    public void RestartGame()
    {
        // Reset the speed increase when the game completely restarts
        EnemyManager.cumulativeSpeedIncrease = 0;

        // Hide the Game Over panel if it's active
        if (gameOverPanel != null && gameOverPanel.activeSelf)
        {
            gameOverPanel.SetActive(false);
        }

        // Reset the TimeScale
        Time.timeScale = 1;

        // Reset the score
        if (PointManager.instance != null)
        {
            PointManager.instance.ResetScore();
        }

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Play music
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayMusic();
        }

        // Reset player lives - This assumes you have a reference to the PlayerLives script
        PlayerLives playerLives = FindObjectOfType<PlayerLives>();
        if (playerLives != null)
        {
            playerLives.ResetLives();
        }
        else
        {
            Debug.LogError("PlayerLives not found in the scene.");
        }
    }

    public void ShowLeaderboard()
    {
        // Find the Leaderboard Panel and set it to active
        leaderboardPanel.SetActive(true);

        // Optionally, if you need to populate or update the leaderboard each time it's shown
        if (Web.instance != null)
        {
            Web.instance.ShowBestScores(gameName);
        }
    }

    public void HideLeaderboard()
    {
        if (leaderboardPanel != null)
        {
            leaderboardPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }


}
