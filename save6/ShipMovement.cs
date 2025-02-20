using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float baseSpeed;
    private float currentSpeed;
    private int direction = 1;  // 1 for right, -1 for left
    public float maxSpeed = 5.0f; // Maximum speed that an enemy ship can reach
    private float lastBoundaryHitTime = 0;
    private float boundaryHitCooldown = 0.1f; // 100 ms cooldown between boundary hits

    private void Start()
    {
        baseSpeed += EnemyManager.cumulativeSpeedIncrease;  // Apply any cumulative speed increase at start
        currentSpeed = Mathf.Abs(baseSpeed) * direction;  // Set current speed based on new base speed
        Debug.Log(gameObject.name + " initialized with base speed: " + baseSpeed + ", current speed: " + currentSpeed);
    }

    public void IncreaseSpeed(float amount)
    {
        baseSpeed += amount;  // Increment baseSpeed by the passed amount
        if (baseSpeed > maxSpeed)
        {
            baseSpeed = maxSpeed; // Cap the speed at maxSpeed if it exceeds
        }
        currentSpeed = Mathf.Abs(baseSpeed) * direction;  // Update current speed based on new base speed
        Debug.Log(gameObject.name + " speed increased to: " + baseSpeed + ", current speed: " + currentSpeed);
    }

    void Update()
    {
        // Move horizontally based on current speed and direction
        transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary" && Time.time > lastBoundaryHitTime + boundaryHitCooldown)
        {
            lastBoundaryHitTime = Time.time;
            // Move down and reverse direction
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            direction *= -1;  // Reverse the direction
            currentSpeed = Mathf.Abs(baseSpeed) * direction;  // Ensure current speed reflects the new direction
        }
    }
}
