using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletSpawnData[] spawnDatas;
    int index = 0;
    public bool isSequenceRandom;
    public bool spawningAutomatically;
    BulletSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }
    
    float timer;
    bool shooting;


    float[] rotations;
    void Start()
    {
        timer = GetSpawnData().cooldown;
        rotations = new float[GetSpawnData().numberOfBullets];
        if (!GetSpawnData().isRandom)
        {
            /* 
             * This doesn't need to be in update because the rotations will be the same no matter what
             * Unless if we change min Rotation and max Rotation Variables leave this in Start.
             */
            DistributedRotations();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawningAutomatically)
        {
            if (shooting)
            {
                if (timer <= 0)
                {
                    SpawnBullets();
                    timer = GetSpawnData().cooldown;
                    if (isSequenceRandom)
                    {
                        index = Random.Range(0, spawnDatas.Length);
                    }
                    else
                    {
                        index += 1;
                        if (index >= spawnDatas.Length) index = 0;
                    }
                    rotations = new float[GetSpawnData().numberOfBullets];
                }
                timer -= Time.deltaTime;
            }
        }
    }

    public void Shoot(bool isShooting)
    {
        shooting = isShooting;
    }

    // Select a random rotation from min to max for each bullet
    public float[] RandomRotations()
    {
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            rotations[i] = Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }
        return rotations;

    }

    // This will set random rotations evenly distributed between the min and max Rotation.
    public float[] DistributedRotations()
    {
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            var fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation; // We add minRotation to undo Difference
        }
        return rotations;
    }
    public GameObject[] SpawnBullets()
    {
        rotations = new float[GetSpawnData().numberOfBullets];
        if (GetSpawnData().isRandom)
        {
            // This is in Update because we want a random rotation for each bullet each time
            RandomRotations();
        }

        // Spawn Bullets
        GameObject[] spawnedBullets = new GameObject[GetSpawnData().numberOfBullets];
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            spawnedBullets[i] = BulletManager.GetBulletFromPool();
            if (spawnedBullets[i] = null)
            {
                spawnedBullets[i] = Instantiate(GetSpawnData().bulletResource, transform);
            }
            else
            {
                spawnedBullets[i].transform.SetParent(transform);
                spawnedBullets[i].transform.localPosition = Vector2.zero;
            }
            

            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.speed = GetSpawnData().bulletSpeed;
            b.velocity = GetSpawnData().bulletVelocity;
            if (GetSpawnData().isNotParent)
            {
                spawnedBullets[i].transform.SetParent(null);
            }
        }
        return spawnedBullets;
    }
}
