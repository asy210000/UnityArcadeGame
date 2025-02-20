using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public Web Web;
    public UserInfo userInfo; // Instance of UserInfo

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            Web = GetComponent<Web>();
            userInfo = new UserInfo(); // Initialize the UserInfo object
            DontDestroyOnLoad(gameObject);
        }
    }
}

[System.Serializable] // This makes it visible in the Unity inspector if needed
public class UserInfo
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public int ID { get; private set; }

    public void SetCredentials(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public void SetID(string idText)
    {
        if (int.TryParse(idText, out int parsedId))
        {
            ID = parsedId;
        }
        else
        {
            Debug.LogError("Failed to parse ID.");
        }
    }
}
