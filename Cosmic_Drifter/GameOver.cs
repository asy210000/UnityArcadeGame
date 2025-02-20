using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            gameOverPanel.SetActive(true);
            StopAllGameplay();
        }
    }

    private void StopAllGameplay()
    {
        // Stop obstacle spawning
        SpawnObstacles[] spawners = FindObjectsOfType<SpawnObstacles>();
        foreach (var spawner in spawners)
        {
            spawner.enabled = false; // This stops new obstacles from spawning
        }

        // Destroy existing obstacles
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject); // This cleans up existing obstacles
        }

        BackgroundMusic backgroundMusic = FindObjectOfType<BackgroundMusic>();
        if (backgroundMusic != null)
        {
            backgroundMusic.StopMusic(); // Make sure this method is defined in BackgroundMusic
        }

        CameraMovement cameraMovement = FindObjectOfType<CameraMovement>();
        if (cameraMovement != null)
        {
            cameraMovement.enabled = false;  // Disable script-based movement
            Rigidbody2D rb = cameraMovement.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  // Ensure there's no residual physics-based movement
            }
            Debug.Log("Camera movement stopped");
        }

        LoopingBackground.StopLooping();

    }

    public void Restart()
    {
        // Reset the background looping and other game states before restarting
        LoopingBackground.StartLooping();
        SpawnObstacles.CanSpawn = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        // Implementation to return to the main menu
        SceneManager.LoadScene("MainMenu");
    }
}