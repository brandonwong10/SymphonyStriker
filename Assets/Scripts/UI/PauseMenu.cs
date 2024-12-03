using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pausePanel; // Reference to the pause menu UI
    [SerializeField] private GameObject guidePanel; // Reference to the settings menu UI
    public KeyCode pauseKey; // Key to pause the game
    public bool isPaused = false; // Check if the game is paused

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Pause the game
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Stop the game
        SongManager.instance.PauseSong(0); // Pause the background music
        SongManager.instance.PlaySong(1); // Play the pause music
        isPaused = true;
    }

    // Resume the game
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        SongManager.instance.PauseSong(1); // Pause the pause music
        SongManager.instance.ResumeSong(0); // Resume the background music
        isPaused = false;
    }

    // Opens the guide panel
    public void OpenGuide()
    {
        guidePanel.SetActive(true);
    }

    // Closes the guide panel
    public void CloseGuide()
    {
        guidePanel.SetActive(false);
    }

    // Change the scene based on the passed sceneID
    public void ChangeScene(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
}
