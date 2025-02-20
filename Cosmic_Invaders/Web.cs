using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class Web : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
   {
       // A correct website page.
       //StartCoroutine(GetDate("https://localhost/UnityBackendTutorial/GetDate.php"));
       //StartCoroutine(GetDate());
       //StartCoroutine(GetUsers());
       //StartCoroutine(Login("test", "secret"));
       //StartCoroutine(RegisterUser("testee2", "123456", "test002@aol.com"));

       // A non-existing page.
       //StartCoroutine(GetRequest("https://error.html"));
   }


   public void ShowBestScores() {
      StartCoroutine(GetBestGameScores("test"));
   }


   public IEnumerator Login(string username, string password)
   {
       WWWForm form = new WWWForm();
       form.AddField("loginUser", username);
       form.AddField("loginPass", password);

       //using UnityWebRequest www = UnityWebRequest.Post("https://192.168.1.19/Login.php", form);
       using UnityWebRequest www = UnityWebRequest.Post("https://temoc-arcade.com/Login.php", form);
       //using UnityWebRequest www = UnityWebRequest.Post("https://localhost/UnityBackendTutorial/Login.php", form);
       yield return www.SendWebRequest();

       if (www.result != UnityWebRequest.Result.Success)
       {
           Debug.LogError(www.error);
       }
       else
       {
           Debug.Log(www.downloadHandler.text);  //"Form upload complete!");
           Main.Instance.userInfo.SetCredentials(username, password);
           Main.Instance.userInfo.SetID(www.downloadHandler.text);

        }
    }

   public IEnumerator RegisterUser(string username, string password, string email)
   {
       WWWForm form = new WWWForm();
       form.AddField("loginUser", username);
       form.AddField("loginPass", password);
       form.AddField("loginEmail", email);

       //using UnityWebRequest www = UnityWebRequest.Post("https://192.168.1.19/RegisterUser.php", form);
       using UnityWebRequest www = UnityWebRequest.Post("https://temoc-arcade.com/RegisterUser.php", form);
       //using UnityWebRequest www = UnityWebRequest.Post("https://localhost/UnityBackendTutorial/RegisterUser.php", form);
       yield return www.SendWebRequest();

       if (www.result != UnityWebRequest.Result.Success)
       {
           Debug.LogError(www.error);
       }
       else
       {
           Debug.Log(www.downloadHandler.text);  //"Form upload complete!");
       }
   }

   public IEnumerator GetBestGameScores(string gameName) {
       WWWForm form = new WWWForm();
       form.AddField("gameName", gameName);

       using (UnityWebRequest www = UnityWebRequest.Post("https://temoc-arcade.com/GetBestGameScores.php", form))
       {
         yield return www.SendWebRequest();

         if (www.result != UnityWebRequest.Result.Success)
         {
             Debug.LogError(www.error);
         }
         else
         {
             Debug.Log(www.downloadHandler.text);  //"Form upload complete!");
             string jsonArray = www.downloadHandler.text;

             //Call callback function to pass results
         }
       }
   }

    public IEnumerator SubmitScore(int playerID, string gameName, int score, int playTime, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerID", playerID);
        form.AddField("gameName", gameName);
        form.AddField("score", score);
        form.AddField("playTime", playTime);

        // Log data being sent to the server
        Debug.Log("Sending to server: " + "playerID: " + playerID + ", gameName: " + gameName + ", score: " + score + ", playTime: " + playTime);

        using (UnityWebRequest www = UnityWebRequest.Post("https://temoc-arcade.com/SubmitScore.php", form))
        {
            yield return www.SendWebRequest();

            // Additional logs here
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                callback?.Invoke(www.error);
            }
            else
            {
                Debug.Log("Server response: " + www.downloadHandler.text);
                // Check if server response indicates success
                if (www.downloadHandler.text.Equals("Score successfully submitted!"))
                {
                    Debug.Log("Score submitted successfully!");
                }
                callback?.Invoke(www.downloadHandler.text);
            }
        }
    }




}
