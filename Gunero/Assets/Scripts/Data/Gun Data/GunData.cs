using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObjects/GunData", order = 1)]

public class GunData : ScriptableObject
{
    public GameObject bulletResource;
    public Sprite[] upgradeSprites;
    public float cooldown;
    public float buletSpeed;
}
