using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/RoomSpawnData", order = 1)]

public class RoomSpawnData : ScriptableObject
{
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<Vector2> spawnPositions = new List<Vector2>();
}
