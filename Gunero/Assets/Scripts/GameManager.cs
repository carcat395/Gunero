using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levelIndex = 0;
    public int totalParts = 0;
    public int score = 0;
    private static int machineParts;
    public static bool gameOver;
    public int playerWeapon = 0;
    [SerializeField] bool mainMenu;

    [Space]
    public GameObject uiCanvas;
    UIManager uiManager;

    private void Start()
    {
        if (!mainMenu)
        {
            GameData gd = SaveSystem.LoadGame();
            totalParts = gd.currMoney;
            if (Time.timeScale == 0)
            {
                gameOver = false;
                Time.timeScale = 1f;
            }
            uiManager = uiCanvas.GetComponent<UIManager>();
        }
    }

    public void SetGameOver()
    {
        Time.timeScale = 0;
        gameOver = true;

        score += 100;
        BulletManager.bullets.Clear();
        RoomManager.enemies.Clear();

        uiManager.ActivateGameOverUI();
        Debug.Log("Machine Parts :" + machineParts);
        totalParts += machineParts;
        SaveSystem.SaveGame(this);
    }

    public static void AddToParts(int amount)
    {
        machineParts += amount;
    }
}
