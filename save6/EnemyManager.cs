using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public int totalEnemyCount = 21;
    public float increaseSpeedAmount;
    public static float cumulativeSpeedIncrease;

    private void IncreaseEnemySpeed()
    {
        increaseSpeedAmount = 1.0f; // Ensure this value is set and makes sense for your game's balance.
        cumulativeSpeedIncrease += increaseSpeedAmount;
        Debug.Log("Increasing speed, new cumulative increase: " + cumulativeSpeedIncrease);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            ShipMovement enemyMovement = enemy.GetComponent<ShipMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.IncreaseSpeed(increaseSpeedAmount);  // Pass only the incremental increase
            }
        }
    }

    private void Awake()
    {
        Debug.Log("Existing cumulative speed increase: " + cumulativeSpeedIncrease);
        instance = this;
    }


    public void EnemyDestroyed()
    {
        Debug.Log("Enemy destroyed called. Current count before decrement: " + totalEnemyCount);
        if (totalEnemyCount > 0)
        {
            totalEnemyCount--;
            Debug.Log("Enemy destroyed, remaining: " + totalEnemyCount);
        }

        if (totalEnemyCount <= 0)
        {
            Debug.Log("All enemies destroyed, resetting level.");
            IncreaseEnemySpeed();
            RestartScene();
        }
    }



    public void RestartScene()
    {
        if (WaveNotification.instance != null)
        {
            WaveNotification.instance.ShowNewWaveNotification();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
