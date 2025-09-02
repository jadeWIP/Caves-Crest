using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    // This class handles actions related to the end menu, such as quitting the application, restarting the level, and loading the main menu.
    // Function to quit the application
    public void Quit()
    {
        Application.Quit(); // Quit the application
        Debug.Log("Application Closing..."); // Log a message indicating the application is closing, again this is because it doesn't close in Unity
    }

    // Function to restart the level
    public void Restart()
    {
        SceneManager.LoadScene("Level 1"); // Load the specified scene ("Level 1") to restart the game
    }

    // Function to load the main menu
    public void LoadMenu()
    {
        SceneManager.LoadScene("Start Screen"); // Load the specified scene ("Start Screen") to go back to the main menu
    }
}
