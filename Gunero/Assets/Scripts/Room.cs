using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomNo;
    public int enemiesLeft;
    private bool roomCleared = false;
    public bool finalRoom;

    [Space]
    public GameObject roomManager;
    RoomManager rm;
    public GameObject doorManager;
    DoorManager dm;
    public GameObject gameManager;
    GameManager gm;

    [Space]
    [Header("List Enemy")]
    public GameObject[] enemiesToSpawn;
    public Vector2[] spawnPositions;

    void Start()
    {
        rm = roomManager.GetComponent<RoomManager>();
        dm = doorManager.GetComponent<DoorManager>();
        gm = gameManager.GetComponent<GameManager>();

    }
    
    public void DecreaseEnemyCount()
    {
        enemiesLeft--;
        if (enemiesLeft <= 0)
        {
            if (finalRoom)
            {
                gm.SetGameOver();
            }
            else
            {
                dm.OpenAllDoors();
                ClearRoom();
            }
        }
    }

    public void ActivateRoom()
    {
        if (!roomCleared)
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "Enemy")
                {
                    enemiesLeft++;
                    child.gameObject.SetActive(true);
                    RoomManager.enemies.Add(child.gameObject);
                }
            }

            rm.SetCurrRoom(roomNo);
            rm.SpawnEnemies(roomNo);
            dm.CloseAllDoors();
        }
        else
        {
            return;
        }
    }

    public void ClearRoom()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "Enemy")
                child.SetParent(null);
        }

        roomCleared = true;
    }
}
