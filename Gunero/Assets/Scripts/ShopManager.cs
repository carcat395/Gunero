using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManager : MonoBehaviour
{
    int currAmountOfMoney;
    public int[] weaponProgress;
    public TMP_Text moneyCounter;
    GameManager gm;
    GameData gdata;

    private void Start()
    {
        gm = GetComponent<GameManager>();
        gdata = SaveSystem.LoadGame();
        currAmountOfMoney = gdata.currMoney;

        weaponProgress = gdata.weaponProgress;
        Debug.Log(weaponProgress);
    }

    void LoadSaveFile()
    {
        gm.weaponProgress = gdata.weaponProgress;
        gm.totalParts = gdata.currMoney;
        gm.score = gdata.score;
    }

    private void Update()
    {
        moneyCounter.text = currAmountOfMoney.ToString();
    }

    public int GetCurrAmountOfMoney()
    {
        return currAmountOfMoney;
    }

    public void DecreaseMoney(int amount)
    {
        currAmountOfMoney -= amount;
    }

    public void Back()
    {
        gm.totalParts = currAmountOfMoney;
        gm.weaponProgress = weaponProgress;
        SaveSystem.SaveGame(gm);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    //DEBUG PURPOSE
    public void AddMoney()
    {
        currAmountOfMoney += 100;
    }
}
