using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed;
    public Renderer backgroundRenderer;
    public static bool shouldLoop = true; // This static variable controls the looping
    void Update()
    {
        if (shouldLoop)
        {
            backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
        }
    }

    public static void StopLooping()
    {
        shouldLoop = false;
    }

    public static void StartLooping()
    {
        shouldLoop = true;
    }
    private void Awake()
    {
        // Resets the state when the script instance is created
        StartLooping();
    }
}