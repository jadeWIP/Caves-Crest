using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStick : MonoBehaviour
{
    // This method is called when a collision begins.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is the player.
        if (collision.gameObject.name == "Player")
        {
            // Set the player's parent to the moving platform.
            // This makes the player move with the platform.
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // This method is called when a collision ends.
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the colliding object is the player.
        if (collision.gameObject.name == "Player")
        {
            // Set the player's parent to null, removing the connection to the platform.
            // This allows the player to move independently of the platform.
            collision.gameObject.transform.SetParent(null);
        }
    }
}