using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Make this music manager persist across scenes
            audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Ensure only one instance of the Music Manager exists
        }
    }

    public void PlayMusic()
    {
        if (audioSource != null)
        {
            audioSource.time = 0; // Start from the beginning
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }


    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop(); // Stop the music if it's playing
        }
    }
}
