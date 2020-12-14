using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectManager : MonoBehaviour
{
    int currAmountOfMoney;

    public TMP_Text moneyCounter;

    private void Start()
    {
        GameData gdata = SaveSystem.LoadGame();
        currAmountOfMoney = gdata.currMoney;

        moneyCounter.text = "Curr Money : " + currAmountOfMoney.ToString();
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
