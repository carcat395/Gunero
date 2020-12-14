using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currMoney;
    public int[] score;

    public GameData (GameManager gm)
    {
        currMoney = gm.totalParts;
        /*Debug.Log("level index : " + gm.levelIndex + " score : " + gm.score);
        score[gm.levelIndex] = gm.score;*/
    }
}
