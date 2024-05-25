using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public float timeElapsedSecs = 0;
    public Text timeElapsed;
    // public GameObject gameIsPausedOverlay;

    private bool gameIsPaused = false;
    

    void Update()
    {
//START PAUSE/UNPAUSE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameIsPaused) {
                unpauseGame();
            } else {
                pauseGame();
            }
        }
        if (!gameIsPaused)
        {
            timeElapsedSecs += Time.deltaTime;
            // timeElapsed.text = (Math.Truncate(timeElapsedSecs * 100)/100).ToString();
        }
//END PAUSE/UNPAUSE
        if (Input.GetKeyDown(KeyCode.F))
        {
            
        }

    }

    public void pauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        // gameIsPausedOverlay.SetActive(gameIsPaused);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Reset game
    }
    public void unpauseGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        // gameIsPausedOverlay.SetActive(gameIsPaused);
    }
}
