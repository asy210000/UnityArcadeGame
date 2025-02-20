using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Web : MonoBehaviour
{
    private const string BASE_URL = "https://temoc-arcade.com/";
    private const string LOGIN_ENDPOINT = "Login.php";
    private const string REGISTER_ENDPOINT = "RegisterUser.php";
    private const string SUBMIT_SCORE_ENDPOINT = "SubmitScore.php";
    private const string GET_BEST_SCORES_ENDPOINT = "GetBestGameScores.php";
    public LeaderboardController leaderboardController;
    public static Web instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Example usage, uncomment to test
        // StartCoroutine(Login("test", "secret"));
        // StartCoroutine(RegisterUser("testee2", "123456", "test002@aol.com"));
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + LOGIN_ENDPOINT, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);  // "Form upload complete!"
                Main.Instance.userInfo.SetCredentials(username, password);
                Main.Instance.userInfo.SetID(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password, string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);
        form.AddField("loginEmail", email);

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + REGISTER_ENDPOINT, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);  // "Form upload complete!"
            }
        }
    }

    public IEnumerator GetBestGameScores(string gameName, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameName", gameName);

        // Start of the using block
        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + GET_BEST_SCORES_ENDPOINT, form))
        {
            yield return www.SendWebRequest(); // Wait for the web request to complete

            // Check the result within the using block
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
                callback?.Invoke(null);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                callback?.Invoke(www.downloadHandler.text);
            }
        } // End of the using block
          // No reference to 'www' should exist beyond this point
    }


    public IEnumerator SubmitScore(int playerID, string gameName, int score, int playTime, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", playerID);
        form.AddField("gameName", gameName);
        form.AddField("score", score);
        form.AddField("playTime", playTime);

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL + SUBMIT_SCORE_ENDPOINT, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                callback?.Invoke(www.error);
            }
            else
            {
                Debug.Log("Server response: " + www.downloadHandler.text);
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }

    public void ShowBestScores(string gameName)
    {
        StartCoroutine(GetBestGameScores(gameName, (jsonArray) => {
            Debug.Log($"Received JSON: {jsonArray}");

            // Handle possible response discrepancies
            if (jsonArray == "0" || string.IsNullOrEmpty(jsonArray))
            {
                Debug.LogError("Leaderboard data is empty or null.");
                return;
            }

            // Now parse the JSON
            ScoreEntry[] scores = JsonHelper.FromJson<ScoreEntry>(jsonArray);

            // Check if the parsing was successful
            if (scores == null || scores.Length == 0)
            {
                Debug.LogError("Failed to parse leaderboard data or data is empty.");
                return;
            }

            // Proceed to populate the leaderboard
            if (leaderboardController != null)
            {
                leaderboardController.PopulateLeaderboard(scores);
            }
            else
            {
                Debug.LogError("LeaderboardController reference not set.");
            }
        }));
    }


    private void DisplayScores(ScoreEntry[] scores)
    {
        // Code to update your leaderboard UI
        foreach (var score in scores)
        {
            Debug.Log($"Username: {score.username}, Score: {score.score}");
            // Create or update UI elements here
        }
    }
}
