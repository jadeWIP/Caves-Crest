using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int schmeckle = 0; // Variable to store the schmeckle count.

    [SerializeField] private Text schmecklesText; // Reference to the Text component for displaying the score.
    [SerializeField] private AudioSource collectionSoundEffect; // Reference to the AudioSource for the collection sound effect.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Schmeckle")) ; // Check if the collided object has the tag "Schmeckle".
        {

            collectionSoundEffect.Play(); // Play the collection sound effect.
            Destroy(collision.gameObject); // Destroy the collected schmeckle object.
            schmeckle++; // This just means: schmeckle = schemckle + 1
            schmecklesText.text = "Schmeckles: " + schmeckle; // Update the displayed schmeckle count in the UI.
        }
    }
}
