using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject enemyProjectile;
    public float spawnTimer;
    public float spawnMax = 5;  // Maximum time between spawns
    public float spawnMin = 1;  // Minimum time between spawns

    // Start is called before the first frame update
    void Start()
    {
        // Initialize with a random spawn timer specifically to desynchronize multiple spawners
        spawnTimer = Random.Range(spawnMin, spawnMax);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            Instantiate(enemyProjectile, transform.position, Quaternion.identity);
            // Reset the timer to a random value within defined limits for staggered spawning
            spawnTimer = Random.Range(spawnMin, spawnMax);
        }
    }
}
