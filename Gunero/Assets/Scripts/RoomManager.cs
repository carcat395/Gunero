﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static List<GameObject> enemies = new List<GameObject>();

    int currRoom;
    int prevRoom;
    public GameObject[] rooms;

    public void SetCurrRoom(int roomNo)
    {
        prevRoom = currRoom;
        currRoom = roomNo;
    }

    public void SpawnEnemies(int roomNo)
    {
        GameObject targetRoom = rooms[roomNo];
        Room room = targetRoom.GetComponent<Room>();
        List<GameObject> enemiesToSpawn = room.GetEnemiesToSpawn();
        List<Vector2> spawnPositions = room.GetSpawnPositions();
        
        for(int i = 0; i < enemiesToSpawn.Count; i++)
        {
            GameObject spawnedEnemy = GetEnemiesFromPool(enemiesToSpawn[i].GetComponent<Enemy>().enemyID);
            if ( spawnedEnemy == null)
            {
                spawnedEnemy = Instantiate(enemiesToSpawn[i], spawnPositions[i], Quaternion.identity, targetRoom.transform);
                enemies.Add(spawnedEnemy);
                room.enemiesLeft++;
            }
            else
            {
                spawnedEnemy.transform.SetParent(targetRoom.transform);
                spawnedEnemy.transform.position = spawnPositions[i];
                room.enemiesLeft++;
            }
        }
    }

    public static GameObject GetEnemiesFromPool(string enemyID)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].activeSelf)
            {
                enemies[i].SetActive(true);
                if (enemies[i].GetComponent<Enemy>().enemyID == enemyID)
                {
                    return enemies[i];
                }
                else
                {
                    enemies[i].SetActive(false);
                    continue;
                }
            }    

        }
        return null;
    }
}