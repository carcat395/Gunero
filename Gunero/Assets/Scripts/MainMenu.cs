using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameManager emptygm;

    private void Start()
    {
        emptygm = GetComponent<GameManager>();
        if (SaveSystem.CheckSaveFile())
        {
            Debug.Log("Save File Found");
        }
        else
        {
            SaveSystem.SaveGame(emptygm);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("close game");
    }

    public void ResetProgress()
    {
        SaveSystem.ResetFile(emptygm);
    }
}
