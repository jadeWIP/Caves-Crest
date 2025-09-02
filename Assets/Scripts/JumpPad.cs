using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the JumpPad class, which handles the behavior of a jumping pad
public class JumpPad : MonoBehaviour
{
    private float bounce = 20f; // Set the bounce force for the jump pad
    [SerializeField] AudioSource boing; // Reference to the audio source for the jump pad sound

    // Called when a 2D collision occurs with the jump pad
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            boing.Play(); // Play the jump pad sound effect

            // Apply an upward force to the player using Unity's physics engine
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}
