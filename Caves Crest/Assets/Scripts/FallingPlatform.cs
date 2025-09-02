using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = .5f; // Time delay before the platform falls after player collision
    private float destroyDelay = 2f; // Time delay before the falling platform is destroyed

    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody2D component

    // Called when a 2D collision occurs
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Check if the colliding object is the player
        {
            StartCoroutine(Fall()); // Start the coroutine for the platform to fall
        }
    }

    // Coroutine to handle the falling behavior of the platform
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay); // Wait for the specified delay before making the platform fall
        rb.bodyType = RigidbodyType2D.Dynamic; // Set the Rigidbody2D to Dynamic, allowing gravity to affect the platform
        Destroy(gameObject, destroyDelay); // Destroy the game object after the specified delay
    }
}
