using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectManager : MonoBehaviour
{
    GameManager emptygm;
    public GameObject optionsUI;
    int currAmountOfMoney;

    public TMP_Text moneyCounter;

    private void Start()
    {
        GameData gdata = SaveSystem.LoadGame();
        emptygm = GetComponent<GameManager>();

        if (SaveSystem.CheckSaveFile())
        {
            Debug.Log("Save File Found");
        }
        else
        {
            SaveSystem.SaveGame(emptygm);
        }

        currAmountOfMoney = gdata.currMoney;

        moneyCounter.text = currAmountOfMoney.ToString();
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadShop()
    {
        //getshop buld index
        SceneManager.LoadScene(2);
    }

    public void ResetProgress()
    {
        SaveSystem.ResetFile(emptygm);

    }

    public void ToggleOptions()
    {
        if (optionsUI.activeSelf)
        {
            Debug.Log("close settings");
            optionsUI.SetActive(false);
        }
        else
        {
            Debug.Log("open settings");
            optionsUI.SetActive(true);
        }
    }
}
