using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    enum Gun {Default, AOE, Poison};

    public GameObject[] BulletResource;
    int gunIndex;

    [Space]
    public float cooldown;
    float timer;
    public int numberOfBullets;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
    public int rangeScale;
    public LayerMask enemyLayers;
    Transform nearestEnemy;
    public float shootingAngle;
    bool enemyOnSight;
    Vector2 defaultUp;

    [Space]
    public GunData[] gunDatas; 
    GunData GetGunData(int index)
    {
        return gunDatas[index];
    }

    SpriteRenderer gunRenderer;
    Animator anim;
    Vector2 currPoint;
    Vector2 rangeSize;
    Rigidbody2D m_rigidbody2D;
    Vector3 currPos;
    Vector3 lastPos;
    Transform gun;
    Transform firingPoint;
    Gun currgun;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        timer = cooldown;
        defaultUp = transform.up;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        gun = transform.Find("Gun");
        gunRenderer = gun.GetComponent<SpriteRenderer>();
        firingPoint = gun.Find("Fire Point");
    }

    public void SetWeapon(int weaponIndex, int upgradeProgress)
    {
        switch (weaponIndex)
        {
            case 0:
                gunIndex = weaponIndex;
                currgun = Gun.Default;
                gunRenderer.sprite = GetGunData(weaponIndex).upgradeSprites[upgradeProgress];
                cooldown = GetGunData(weaponIndex).cooldown;
                break;
            case 1:
                gunIndex = weaponIndex;
                currgun = Gun.AOE;
                gunRenderer.sprite = GetGunData(weaponIndex).upgradeSprites[upgradeProgress];
                cooldown = GetGunData(weaponIndex).cooldown;
                break;
            case 2:
                gunIndex = weaponIndex;
                currgun = Gun.Poison;
                gunRenderer.sprite = GetGunData(weaponIndex).upgradeSprites[upgradeProgress];
                cooldown = GetGunData(weaponIndex).cooldown;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currPos = transform.position;
        if (!PlayerMovement.isMoving)
        {
            autoAim();
            if (enemyOnSight)
            {
                gun.right = nearestEnemy.position - transform.position;
            }
            else
            {
                gun.right = defaultUp;
            }

            if (timer <= 0 && enemyOnSight)
            {
                SpawnBullet();
                timer = cooldown;
            }
            timer -= Time.deltaTime;
        }
        else
        {
            gun.up = defaultUp;
        }
        lastPos = currPos;
    }

    public GameObject[] SpawnBullet()
    {
        GameObject[] spawnedBullets = new GameObject[numberOfBullets];
        for (int i = 0; i < numberOfBullets; i++)
        {
            spawnedBullets[i] = BulletManager.GetPlayerBulletFromPool();
            if (spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(GetGunData(gunIndex).bulletResource, gun);
                BulletManager.playerBullets.Add(spawnedBullets[i]);
            }
            else
            {
                spawnedBullets[i].transform.SetParent(gun);
                spawnedBullets[i].transform.localPosition = Vector2.zero;
            }

            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = gun.eulerAngles.z;
            b.speed = GetGunData(gunIndex).buletSpeed;
            b.velocity = bulletVelocity;
            spawnedBullets[i].transform.SetParent(null);
        }
        return spawnedBullets;
        /*
        GameObject spawnedBullet;
        spawnedBullet = Instantiate(GetGunData(gunIndex).bulletResource, gun);

        var b = spawnedBullet.GetComponent<Bullet>();
        b.rotation = gun.eulerAngles.z;
        b.speed = bulletSpeed;
        b.velocity = bulletVelocity;
        spawnedBullet.transform.SetParent(null);

        return spawnedBullet;
        */
    }

    void autoAim()
    {
        float shortestDistance = 0f;

        currPoint = transform.position;
        rangeSize = new Vector2(transform.localScale.x * rangeScale, transform.localScale.y * rangeScale);
        Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(currPoint, rangeSize, 0f, enemyLayers);

        for(int i = 0; i < enemiesInRange.Length; i++)
        {
            float Distance = Vector2.Distance(enemiesInRange[i].transform.position, transform.position);
            if(i == 0)
            {
                nearestEnemy = enemiesInRange[i].transform;
                shortestDistance = Distance;
                enemyOnSight = true;
                anim.SetBool("isShooting", true);
            }
            else if (Distance < shortestDistance)
            {
                nearestEnemy = enemiesInRange[i].transform;
                shortestDistance = Distance;
            }
        }

        if(enemiesInRange.Length == 0)
        {
            enemyOnSight = false;
            anim.SetBool("isShooting", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(currPoint, rangeSize);
    }
}
