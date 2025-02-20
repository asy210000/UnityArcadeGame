using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public static PlayerLives instance; // Singleton instance
    public int lives;
    public Image[] livesUI;
    public GameObject explosionPrefab;
    private const int InitialLives = 3; // Set your initial lives here

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on load
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy this if an instance already exists
            return;
        }
    }

    void Start()
    {
        if (lives == 0) // Initialize lives only if they have not been set
        {
            ResetLives(); // Set lives to initial value when the game starts
        }
        FindAndUpdateLivesUI();
    }

    void OnDestroy()
    {
        // Ensure to unsubscribe only if this is the singleton instance
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This method will be called every time a scene is loaded
        FindAndUpdateLivesUI(); // Only update the UI here, do not reset lives
    }

    void FindAndUpdateLivesUI()
    {
        GameObject livesUIContainer = GameObject.FindGameObjectWithTag("Lives");
        if (livesUIContainer != null)
        {
            livesUI = livesUIContainer.GetComponentsInChildren<Image>();
            UpdateLivesUI();
        }
        else
        {
            Debug.LogError("Lives UI container not found in the scene. Make sure it has the correct tag.");
        }
    }

    public void ResetLives()
    {
        lives = InitialLives; // Reset to initial lives
        UpdateLivesUI(); // Update the lives UI to reflect the change
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
            else
            {
                Debug.LogError("Player Lives: GameManager instance is null.");
            }
        }
    }

    void UpdateLivesUI()
    {
        if (livesUI != null)
        {
            for (int i = 0; i < livesUI.Length; i++)
            {
                livesUI[i].enabled = i < lives;
            }
        }
        else
        {
            Debug.LogError("Lives UI array is null. Make sure the Lives UI container is found and Images are assigned.");
        }
    }
}
