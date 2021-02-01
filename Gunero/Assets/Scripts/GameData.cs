using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int weaponIndex;
    public int currMoney;
    public int[] weaponProgress;
    public int[] score;

    public GameData (GameManager gm)
    {
        currMoney = gm.totalParts;
        weaponProgress = gm.weaponProgress;
        /*Debug.Log("level index : " + gm.levelIndex + " score : " + gm.score);
        score[gm.levelIndex] = gm.score;*/
    }
}
