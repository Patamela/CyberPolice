using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public static event Action<int> OnCoinCollected; // Event to notify when a coin is collected
    Rigidbody2D rb;

    bool hasTarget;
    Vector3 targetPosition;
    float moveSpeed = 50f; // Speed at which the coin moves towards the target position

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Collect()
    {
        Destroy(gameObject);
        OnCoinCollected?.Invoke(100);
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized; // Calculate the direction towards the target position
            rb.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed; // Move the coin towards the target position
        }
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true; // Set the flag to indicate that the coin has a target position
    }
}
