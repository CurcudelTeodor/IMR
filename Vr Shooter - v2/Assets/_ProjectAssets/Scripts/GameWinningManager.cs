using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameWinningManager : MonoBehaviour
{
    private PlayerController playerController;
    public Camera playerCamera;


    public WaveSpawner waveSpawner;
    public GameObject gameWinCanvas;
    public TextMeshProUGUI scoreText;

    private bool hasWon = false;

    void Start()
    {
        // Disable the game win canvas initially
        gameWinCanvas.SetActive(false);
        playerController = playerCamera.GetComponent<PlayerController>();

    }

    void Update()
    {
        // Check if all waves are completed and there are no enemies alive
        if (waveSpawner.AllWavesCompleted() && waveSpawner.AllEnemiesDefeated() && !hasWon)
        {   
            // Show the game win canvas
            gameWinCanvas.SetActive(true);

            // Display the score
            int score = CalculateScore(); // You need to implement this function to calculate the score
            scoreText.text = "Congratulations!! You won!\nScore = " + score;

            // Freeze the game by setting the time scale to 0
            //Time.timeScale = 0f;

            hasWon = true; // Prevents the win screen from being displayed multiple times
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

    // You need to implement this function to calculate the score based on your game's logic
    private int CalculateScore()
    {
        int score = playerController.GetScore();
        return score;
    }
}
