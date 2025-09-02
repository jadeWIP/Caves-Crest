using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to the player's Rigidbody2D component.
    private Animator anim; // Reference to the player's Animator component.
    private BoxCollider2D bc; // Reference to the player's BoxCollider2D component.

    [SerializeField] private AudioSource deathSoundEffect; // Sound effect played on player death.

    private void Start()
    {
        // Assigning references to the necessary components when the game starts.
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checking if the player collides with an object tagged as "Trap".
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die(); // Call the Die() method when a trap is encountered.
        }
    }

    private void Die()
    {
        deathSoundEffect.Play(); // Death Sound
        rb.bodyType = RigidbodyType2D.Static; // RigidBody2D turned Off
        bc.enabled = false; // Collison Turned Off
        anim.SetTrigger("death"); // Death Animation
    }

    private void RestartLevel()
    {
        // Reloading the current scene when the player needs to restart.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        bc.enabled = true; // Re-enabling the player's collision after restarting.
    }
}
