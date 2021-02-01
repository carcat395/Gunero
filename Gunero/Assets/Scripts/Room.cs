using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int roomNo;
    int wavesLeft;
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
    public RoomSpawnData[] spawnDatas;
    int index = 0;

    RoomSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }

    void Start()
    {
        rm = roomManager.GetComponent<RoomManager>();
        dm = doorManager.GetComponent<DoorManager>();
        gm = gameManager.GetComponent<GameManager>();
        wavesLeft = spawnDatas.Length;
        Debug.Log("waves left: " + spawnDatas.Length);

    }
    
    public void AssignTarget(Transform target)
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "Enemy")
            {
                child.GetComponent<EnemyAI>().target = target;
            }
        }
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
                ClearRoom();
            }
        }
    }

    public void ActivateRoom()
    {
        if (!roomCleared)
        {//activate enemy as first wave
         /*   foreach (Transform child in transform)
            {
                if (child.tag == "Enemy")
                {
                    enemiesLeft++;
                    child.gameObject.SetActive(true);
                    RoomManager.enemies.Add(child.gameObject);
                }
            }
            */
            Debug.Log("Spawn: " + GetSpawnData().name);
            rm.SetCurrRoom(roomNo);
            rm.SpawnEnemies(roomNo);
            dm.CloseAllDoors();
            wavesLeft--;
        }
        else
        {
            return;
        }
    }

    public List<GameObject> GetEnemiesToSpawn()
    {
        return GetSpawnData().enemiesToSpawn;
    }

    public List<Vector2> GetSpawnPositions()
    {
        return GetSpawnData().spawnPositions;
    }

    public void ClearRoom()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == "Enemy")
                child.SetParent(null);
        }

        if (wavesLeft > 0)
        {
            index++;
            ActivateRoom();
        }
        else
        {
            dm.OpenAllDoors();
            roomCleared = true;
        }
    }
}
