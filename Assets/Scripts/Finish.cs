using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource finishSound; // Reference to the audio source for entering the next level

    // This method is automatically called when a Collider2D enters the trigger zone of this GameObject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the GameObject that entered the trigger zone has the tag "Finish"
        if (collision.gameObject.CompareTag("Finish")) ;
        {
            CompleteLevel(); // Call the method to complete the level if the condition is met
        }
    }

    // Method to load the next scene in the build index
    private void CompleteLevel()
    {
        // Get the current scene's build index and load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}