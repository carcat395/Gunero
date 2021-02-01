using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject pauseUI;
    public GameObject gameOverUI;
    public GameObject audioManager;

    AudioManager audio;

    private void Start()
    {
        audio = audioManager.GetComponent<AudioManager>();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        audio.GetAudioSource("BGM").Pause();
        gameUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        audio.GetAudioSource("BGM").UnPause();
        gameUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ActivateGameOverUI()
    {
        gameUI.SetActive(false);
        gameOverUI.SetActive(true);
    }
}
