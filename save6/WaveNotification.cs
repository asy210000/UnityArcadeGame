using UnityEngine;
using TMPro; // Include the TextMeshPro namespace
using System.Collections;

public class WaveNotification : MonoBehaviour
{
    public static WaveNotification instance;  // Singleton instance
    public TextMeshProUGUI waveNotificationText;  // Update this reference to TextMeshProUGUI
    public float notificationDuration = 1.0f;  // Duration for how long the notification will be shown

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Make this object persistent
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Ensures only one instance is active
        }
    }

    void Start()
    {
        if (waveNotificationText != null)
        {
            waveNotificationText.enabled = false;  // Start with the notification hidden
        }
        else
        {
            Debug.LogError("WaveNotification: No waveNotificationText linked in the inspector.");
        }
    }

    public void ShowNewWaveNotification()
    {
        if (waveNotificationText != null)
        {
            StartCoroutine(DisplayWaveNotification());
        }
        else
        {
            Debug.LogError("WaveNotification: No waveNotificationText linked in the inspector.");
        }
    }

    IEnumerator DisplayWaveNotification()
    {
        waveNotificationText.enabled = true;
        waveNotificationText.text = "New wave! Faster enemy ships!";
        yield return new WaitForSeconds(notificationDuration);
        waveNotificationText.enabled = false;
    }
}
