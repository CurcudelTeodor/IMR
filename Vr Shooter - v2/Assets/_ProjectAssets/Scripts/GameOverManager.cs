using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private PlayerController playerController;
    public Camera playerCamera;

    public GameObject gameOverCanvas;

    void Start()
    {
        // Disable the game over canvas initially
        gameOverCanvas.SetActive(false);
        playerController = playerCamera.GetComponent<PlayerController>();

    }

    void Update()
    {
        // Check if the player's health is 0
        if (playerController.currentHealth <= 0)
        {
            // Show the game over canvas
            gameOverCanvas.SetActive(true);

            // Freeze the game by setting the time scale to 0
            Time.timeScale = 0f;

            // Check for input to restart or exit
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // Reload the current scene
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
    }

    // Called by the "Play Again" button
    public void PlayAgain()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called by the "Exit" button
    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
