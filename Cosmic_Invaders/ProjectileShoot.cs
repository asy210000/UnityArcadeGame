using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Drag your projectile prefab here in the Inspector
    private float lastShotTime = 0;
    public float fireRate = 0.05f;       // Time between shots in seconds

    void Update()
    {
        // Check if the player presses the fire button and enough time has elapsed since the last shot
        if (Input.GetButton("Fire1") && Time.time > lastShotTime + fireRate)
        {
            ShootProjectile();
            lastShotTime = Time.time; // Update lastShotTime to the current time
        }
    }

    void ShootProjectile()
    {
        // Instantiate a projectile at the shooting point's position and rotation
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
