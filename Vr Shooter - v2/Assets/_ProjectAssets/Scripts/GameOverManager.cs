using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    private PlayerController playerController;
    public Camera playerCamera;

    public GameObject gameOverCanvas;
    public TextMeshProUGUI scoreText; // Reference to the Text component displaying the score


    void Start()
    {
        // Disable the game over canvas initially
        gameOverCanvas.SetActive(false);
        playerController = playerCamera.GetComponent<PlayerController>();
        //Score = 

    }

    void Update()
    {
        // Check if the player's health is 0
        if (playerController.currentHealth <= 0)
        {
            int score = playerController.GetScore();

            // Show the game over canvas
            gameOverCanvas.SetActive(true);

            // Display the score
            scoreText.text = "Score = " + score;

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
        // Reload the current scene asynchronously
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).completed += OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(AsyncOperation asyncOperation)
    {   
        // Reset the time scale when the scene is loaded
        Time.timeScale = 1f;
    }

    // Called by the "Exit" button
    public void ExitGame()
    {
        // Quit the application
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
