using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed = 5;
    public float hInput;
    public int health = 3;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);  // Destroy if another instance already exists
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this instance across scenes
        }
    }

    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(Vector2.right * hInput * moveSpeed * Time.deltaTime);
    }
}
