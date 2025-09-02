using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Flag to track whether the game is currently paused
    public GameObject pauseMenuUI; // Reference to the UI element representing the pause menu

    // Update is called once per frame
    void Update()
    {
        // Check for the Escape key to toggle between pausing and resuming the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                // If the game is already paused, resume it
                Resume();
            }
            else
            {
                // If the game is not paused, pause it
                Pause();
            }
        }
    }

    // Method to resume the game
    public void Resume ()
    {
        pauseMenuUI.SetActive(false); // Deactivate the pause menu UI
        Time.timeScale = 1f; // Set the time scale back to 1 to resume normal time flow
        GameIsPaused = false; // Update the GameIsPaused flag to indicate the game is no longer paused
    }

    // Method to pause the game
    void Pause ()
    {
        pauseMenuUI.SetActive(true); // Activate the pause menu UI
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        GameIsPaused = true; // Update the GameIsPaused flag to indicate the game is currently paused
    }

    // Method to load the main menu scene
    public void LoadMenu()
    {
        Time.timeScale = 1f; // Set the time scale back to 1 before loading the menu to resume normal time flow
        SceneManager.LoadScene("Start Screen"); // Load the specified scene ("Start Screen" in this case)
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting game..."); // Log a message indicating the game is quitting, this is here because the game does not quit in Unity.
        Application.Quit(); // Quit the application
    }
}
