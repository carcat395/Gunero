using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTester : MonoBehaviour
{
    public GameObject bulletResource;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;
    public bool isRandom;
    public bool isNotParent;

    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
    
    public string bulletName;
    private int testSpawned = 0;
    List<GameObject> spawnedTestBullets = new List<GameObject>();

    public void IncrementTestNo()
    {
        testSpawned++;
    }

    float[] rotations;

    public void SpawnTest()
    {
        timer = cooldown;
        rotations = new float[numberOfBullets];
        if (!isRandom)
        {
            DistributedRotations();
        }

        SpawnBullets();
    }

    public float[] RandomRotations()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            rotations[i] = Random.Range(minRotation, maxRotation);
        }
        return rotations;

    }
    
    public float[] DistributedRotations()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            var fraction = (float)i / ((float)numberOfBullets - 1);
            var difference = maxRotation - minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + minRotation;
        }
        foreach (var r in rotations) print(r);
        return rotations;
    }

    public GameObject[] SpawnBullets()
    {
        if (isRandom)
        {
            RandomRotations();
        }
        
        GameObject[] spawnedBullets = new GameObject[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            spawnedBullets[i] = Instantiate(bulletResource, transform);

            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;
            b.transform.Translate(VectorFromAngle(rotations[i]) * b.speed * cooldown * testSpawned);
            Debug.Log(testSpawned);

            spawnedTestBullets.Add(spawnedBullets[i]);
        }
        return spawnedBullets;
    }

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos((theta * Mathf.PI) / 180), Mathf.Sin((theta * Mathf.PI) / 180));
    }

    public void ClearBullets()
    {
        foreach(GameObject b in spawnedTestBullets)
        {
            DestroyImmediate(b);
        }
        testSpawned = 0;
    }

    public void CreateScriptableObject()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, VectorFromAngle(minRotation));
        Gizmos.DrawLine(transform.position, VectorFromAngle(maxRotation));
    }
}
