using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuElements; // Reference to the parent GameObject holding pause menu elements

    private bool isPaused = false;

    void Start()
    {
        // Ensure pause menu elements are hidden at the start
        SetPauseMenuVisibility(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        // Hide pause menu elements
        SetPauseMenuVisibility(false);

        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        // Show pause menu elements
        SetPauseMenuVisibility(true);

        // Pause the game
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ContinueGame()
    {
        // Hide pause menu elements
        SetPauseMenuVisibility(false);

        // Resume the game
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        // Hide pause menu elements
        SetPauseMenuVisibility(false);

        // Resume the game
        Time.timeScale = 1f;

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the name of your main menu scene
    }

    private void SetPauseMenuVisibility(bool visible)
    {
        // Set the active state of the pause menu elements
        pauseMenuElements.SetActive(visible);
    }
}
