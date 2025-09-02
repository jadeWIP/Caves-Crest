using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private GameObject currentOneWayPlatform; // Reference to the currently collided one-way platform

    [SerializeField] private BoxCollider2D playerCollider; // Reference to the player's BoxCollider2D

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) // Check for the 'S' key or Down Arrow key press
        {
            if (currentOneWayPlatform != null) // Check if a one-way platform is currently collided
            {
                StartCoroutine(DisableCollision()); // Disable collision with the one-way platform temporarily
            }
        }
    }

    // Called when the player collides with an object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform")) // Check if the collided object has the "OneWayPlatform" tag
        {
            currentOneWayPlatform = collision.gameObject; // Set the currently collided one-way platform
        }
    }

    // Called when the player stops colliding with an object
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform")) // Check if the stopped colliding object has the "OneWayPlatform" tag
        {
            currentOneWayPlatform = null; // Reset the currently collided one-way platform
        }
    }

    // Coroutine to temporarily disable collision between player and one-way platform
    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component of the one-way platform

        Physics2D.IgnoreCollision(playerCollider, platformCollider); // Ignore collision between player and one-way platform
        yield return new WaitForSeconds(0.25f); // Wait for a short duration (0.25 seconds)
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false); // Re-enable collision between player and one-way platform

    }
}
