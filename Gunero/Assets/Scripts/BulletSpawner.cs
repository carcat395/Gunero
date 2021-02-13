using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletSpawnData[] spawnDatas;
    public int index = 0;
    public bool singleShot;
    public bool isSequenceRandom;
    public bool inSequence;
    public bool spawningAutomatically;
    BulletSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }
    
    float timer;
    bool shooting;
    Transform firingPoint;
    public bool tracking;


    float[] rotations;
    void Start()
    {
        timer = GetSpawnData().cooldown;
        rotations = new float[GetSpawnData().numberOfBullets];
        if(!GetSpawnData().isRandom)
        {
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
                    else if(inSequence)
                    {
                        index++;
                        if (index >= spawnDatas.Length) index = 0;
                    }
                    rotations = new float[GetSpawnData().numberOfBullets];
                }
                timer -= Time.deltaTime;
            }
        }
    }

    public void Shoot(bool isShooting, Transform firePoint)
    {
        shooting = isShooting;
        firingPoint = firePoint;
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
            float fraction;
            if (GetSpawnData().numberOfBullets == 1)
            {
                fraction = (float)i / (2);
            }
            else
            {
                fraction = (float)i / ((float)GetSpawnData().numberOfBullets - 1);
            }
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation; // We add minRotation to undo Difference
        }
        return rotations;
    }

    public void ChangeIndex(int i)
    {
        index = i;
    }

    public GameObject[] SpawnBullets()
    {
        rotations = new float[GetSpawnData().numberOfBullets];
        if (GetSpawnData().isRandom)
        {
            RandomRotations();
        }
        else
        {
            DistributedRotations();
        }

        // Spawn Bullets
        GameObject[] spawnedBullets = new GameObject[GetSpawnData().numberOfBullets];
        for (int i = 0; i < GetSpawnData().numberOfBullets; i++)
        {
            spawnedBullets[i] = BulletManager.GetEnemyBulletFromPool();
            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(GetSpawnData().bulletResource, firingPoint);
                BulletManager.enemyBullets.Add(spawnedBullets[i]);
            }
            else
            {
                spawnedBullets[i].transform.SetParent(firingPoint);
                spawnedBullets[i].transform.localPosition = Vector2.zero;
            }
            
            var b = spawnedBullets[i].GetComponent<Bullet>();
            if (tracking)
            {
                b.rotation = rotations[i] + firingPoint.eulerAngles.z + 225;
            }
            else
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