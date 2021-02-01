using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RoomCreator : EditorWindow
{
    string enemySet;
    int roomCount;
    GameObject newRoom;
    GameObject entryPoint;
    GameObject enemyToSpawn;

    List<GameObject> enemyMarkers = new List<GameObject>();
    List<GameObject> enemiesToSpawn = new List<GameObject>();

    [MenuItem("Window/Room Creator")]
    public static void ShowWindow()
    {
        GetWindow<RoomCreator>("Room Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Room Creator v0.1", EditorStyles.boldLabel);

        GUILayout.Space(5);
        if (GUILayout.Button("Make New Room"))
        {
            MakeNewRoom();
        }

        GUILayout.Space(10);
        enemySet = EditorGUILayout.TextField("Enemy Set", enemySet);
        enemyToSpawn = (GameObject)EditorGUILayout.ObjectField("Enemy to spawn", enemyToSpawn, typeof(GameObject), false);

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("New Enemy"))
        {
                if (enemyToSpawn == null)
                {
                    Debug.LogError("please specify an enemy to spawn first");
                }
                else
                {
                    GameObject spawnedMarker = Instantiate(enemyToSpawn);
                    enemiesToSpawn.Add(enemyToSpawn);
                    enemyMarkers.Add(spawnedMarker);
                }
        }

        if (GUILayout.Button("Clear Enemies"))
        {
            ClearEnemyList();
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create Scriptable Object"))
        {
            RoomSpawnData roomSO = ScriptableObject.CreateInstance<RoomSpawnData>();
            AssetDatabase.CreateAsset(roomSO, "Assets/" + enemySet + ".asset");
            Debug.Log(roomSO);

            for (int i = 0; i < enemyMarkers.Count; i++)
            {
                roomSO.enemiesToSpawn.Add(enemiesToSpawn[i]);
                Debug.Log(enemyMarkers[i]);
                roomSO.spawnPositions.Add(enemyMarkers[i].transform.position);
                Debug.Log("added");
            }
            ClearEnemyList();
        }
    }

    void ClearEnemyList()
    {
        foreach (GameObject e in enemyMarkers)
        {
            DestroyImmediate(e);
        }
        enemyMarkers.Clear();
        enemiesToSpawn.Clear();
    }

    void MakeNewRoom()
        {
            roomCount = 2;
            roomCount++;

            newRoom = new GameObject("Room" + roomCount);
            newRoom.AddComponent<Room>();
            newRoom.GetComponent<Room>().roomNo = roomCount;

            entryPoint = new GameObject("Entry Point");
            entryPoint.transform.SetParent(newRoom.transform);
            entryPoint.AddComponent<BoxCollider2D>();
        entryPoint.GetComponent<BoxCollider2D>().isTrigger = true;
            entryPoint.AddComponent<EntryPoint>();
        }
}
