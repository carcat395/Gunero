using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject BulletResource;
    //public GameObject AttackRange;

    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
    public int rangeScale;
    public LayerMask enemyLayers;
    Transform nearestEnemy;
    public float shootingAngle;
    bool enemyOnSight;
    Vector2 defaultUp;

    Vector2 currPoint;
    Vector2 rangeSize;
    Rigidbody2D m_rigidbody2D;
    Vector3 currPos;
    Vector3 lastPos;

    // Start is called before the first frame update
    private void Awake()
    {
        timer = cooldown;
        defaultUp = transform.up;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
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
                transform.up = nearestEnemy.position - transform.position;
            }
            else
            {
                transform.up = defaultUp;
            }

            if (timer <= 0)
            {
                SpawnBullet();
                timer = cooldown;
            }
            timer -= Time.deltaTime;
        }
        else
        {
            transform.up = defaultUp;
        }
        lastPos = currPos;
    }

    public GameObject SpawnBullet()
    {
        GameObject spawnedBullet;
        spawnedBullet = Instantiate(BulletResource, transform);

        var b = spawnedBullet.GetComponent<Bullet>();
        b.rotation = 90f + transform.eulerAngles.z;
        b.speed = bulletSpeed;
        b.velocity = bulletVelocity;
        spawnedBullet.transform.SetParent(null);

        return spawnedBullet;
        
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
            }
            else if (Distance < shortestDistance)
            {
                nearestEnemy = enemiesInRange[i].transform;
                shortestDistance = Distance;
            }
        }
        if(enemiesInRange == null)
        {
            enemyOnSight = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(currPoint, rangeSize);
    }
}
