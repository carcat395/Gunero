using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletTester))]
[CanEditMultipleObjects]
public class BulletTesterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BulletTester bt = (BulletTester)target;

        GUILayout.BeginHorizontal();
            if(GUILayout.Button("Test Bullets"))
        {

            bt.IncrementTestNo();
            bt.SpawnTest();
        }
            if(GUILayout.Button("Clear Bullets"))
        {
            bt.ClearBullets();
        }
        GUILayout.EndHorizontal();

        if(GUILayout.Button("Create Scriptable Object"))
        {
            BulletSpawnData bulletSO = ScriptableObject.CreateInstance<BulletSpawnData>();
            AssetDatabase.CreateAsset(bulletSO, "Assets/" + bt.bulletName + ".asset");
            bulletSO.bulletResource = bt.bulletResource;
            bulletSO.bulletSpeed = bt.bulletSpeed;
            bulletSO.bulletVelocity = bt.bulletVelocity;
            bulletSO.cooldown = bt.cooldown;
            bulletSO.isNotParent = bt.isNotParent;
            bulletSO.isRandom = bt.isRandom;
            bulletSO.maxRotation = bt.maxRotation;
            bulletSO.minRotation = bt.minRotation;
            bulletSO.numberOfBullets = bt.numberOfBullets;


            EditorUtility.FocusProjectWindow();

            Selection.activeObject = bulletSO;
        }
    }
}
