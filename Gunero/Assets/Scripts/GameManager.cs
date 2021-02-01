using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int[] weaponProgress = new int[3];
    public int levelIndex = 0;
    public int totalParts = 0;
    public int[] score = new int[5];
    private static int machineParts;
    public int weaponIndex = 0;
    public static bool gameOver;
    [SerializeField] bool menu;

    [Space]
    public GameObject uiCanvas;
    public GameObject audioManager;
    UIManager uiManager;

    private void Start()
    {
        if (!menu)
        {
            GameData gd = SaveSystem.LoadGame();
            totalParts = gd.currMoney;
            weaponProgress = gd.weaponProgress;
            if (Time.timeScale == 0)
            {
                gameOver = false;
                Time.timeScale = 1f;
            }
            uiManager = uiCanvas.GetComponent<UIManager>();
        }

        weaponIndex = WeaponSelectUI.GetSelectedGun();
    }

    public void SetGameOver()
    {
        audioManager.GetComponent<AudioManager>().Stop("BGM");
        Time.timeScale = 0;
        gameOver = true;

        //score[levelIndex] += 100;
        BulletManager.enemyBullets.Clear();
        BulletManager.playerBullets.Clear();
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
