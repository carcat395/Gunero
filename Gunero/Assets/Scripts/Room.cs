using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Room : MonoBehaviour
{
    public int roomNo;
    int wavesLeft;
    public int enemiesLeft;
    private bool roomCleared = false;
    public bool finalRoom;
    public bool bossRoom;

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

    Transform target;
    public float cameraSizeModifier;

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
    
    public void AssignTarget(Transform pTarget)
    {
        target = pTarget;
        foreach(Transform child in transform)
        {
            if(child.tag == "Enemy")
            {
                child.GetComponent<EnemyAI>().target = target;
            }
            else if(child.tag == "Boss")
            {
                child.GetComponentInChildren<Boss>().target = target.gameObject;
                child.GetComponentInChildren<Animator>().SetBool("Liver", true);
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
            if (bossRoom)
            {
                target.GetComponent<Player>().PausePlayer();
                dm.CloseAllDoors();
            }
            else
            {
                Debug.Log("Spawn: " + GetSpawnData().name);
                rm.SetCurrRoom(roomNo);
                rm.SpawnEnemies(roomNo);
                dm.CloseAllDoors();
                wavesLeft--;
            }

            ModifyCameraSize(0, cameraSizeModifier);
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

    void ModifyCameraSize(float sizeIncrement, float targetCameraSize)
    {
        var camera = Camera.main;
        var brain = (camera == null) ? null : camera.GetComponent<CinemachineBrain>();
        var vcam = (brain == null) ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if (vcam != null)
            vcam.m_Lens.OrthographicSize += sizeIncrement;

        if (sizeIncrement < targetCameraSize)
            ModifyCameraSize(sizeIncrement + 1, targetCameraSize);
        else if (sizeIncrement > targetCameraSize)
            ModifyCameraSize(sizeIncrement - 1, targetCameraSize);
        else
            return;
    }
}
