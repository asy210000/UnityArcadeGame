using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    public void OnLoginSuccessful(string username)
    {
        // This function gets called when the player successfully logs in
        // Let's assume GetPlayerIDFromUsername is a function that takes a username
        // and returns the associated player ID from your game's system
        //int playerID = 9; //GetPlayerIDFromUsername(username);
        //PointManager.instance.playerID = playerID; // Set the playerID in PointManager
    }
}
