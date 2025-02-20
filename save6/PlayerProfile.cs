using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public static PlayerProfile instance;
    public int playerID = 12;  // Hardcoded for testing

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
        }
    }
}
