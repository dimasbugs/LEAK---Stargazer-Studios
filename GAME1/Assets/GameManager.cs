using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    private bool isGamePaused = false;

    void Start()
    {
        // Ensure the game starts with the Time.timeScale set to 1 (normal speed)
        Time.timeScale = 1f;
    }

    void Update()
    {
        // Check for input to pause/unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void PlayerDied()
    {
        // Set the game over screen object to not null
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Reload the current scene when the player chooses to restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Unpause the game
        Time.timeScale = 1f;
    }

    void TogglePause()
    {
        // Toggle the game's pause state
        isGamePaused = !isGamePaused;

        // Adjust time scale based on pause state
        Time.timeScale = isGamePaused ? 0f : 1f;
    }
}
