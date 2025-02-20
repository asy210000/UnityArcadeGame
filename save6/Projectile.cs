using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public GameObject explosionPrefab;
    private PointManager pointManager;
    private AudioSource audioSource;
    private bool hasHit = false; //check hit status

    // Start is called before the first frame update
    void Start()
    {
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHit && collision.gameObject.tag == "Enemy")
        {
            hasHit = true; // Mark the projectile as having hit
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            pointManager.UpdateScore(50);
            Destroy(gameObject); // Immediately destroy the projectile
            EnemyManager.instance.EnemyDestroyed(); // Notify the destruction of an enemy
            Destroy(explosionInstance, 1.0f); // Destroy the explosion after 1 second
        }

        if (collision.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }

}