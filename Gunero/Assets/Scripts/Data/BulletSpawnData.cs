using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletSpawnData", order = 1)]

public class BulletSpawnData : ScriptableObject
{
    public GameObject bulletResource;
    public float minRotation;
    public float maxRotation;
    public int numberOfBullets;
    public bool isRandom;
    public bool isNotParent;
    public float cooldown;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
}
