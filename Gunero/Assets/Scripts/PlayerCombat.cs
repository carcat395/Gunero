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
    //float angle;
    public int rangeScale;
    public LayerMask enemyLayers;
    Transform nearestEnemy;

    Vector2 currPoint;
    Vector2 rangeSize;

    // Start is called before the first frame update
    private void Awake()
    {
        timer = cooldown;
    }
    
    // Update is called once per frame
    void Update()
    {
        autoAim();
        transform.up = nearestEnemy.position - transform.position;

        if (timer <= 0)
        {
            SpawnBullet();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }

    public GameObject SpawnBullet()
    {
        GameObject spawnedBullet;
        spawnedBullet = Instantiate(BulletResource, transform);

        var b = spawnedBullet.GetComponent<Bullet>();
        b.rotation = Vector2.Angle(nearestEnemy.position, transform.position) - 90f;
        b.speed = bulletSpeed;
        b.velocity = bulletVelocity;
        
        return spawnedBullet;
    }

    void autoAim()
    {
        float shortestDistance = 100f;

        currPoint = transform.position;
        rangeSize = new Vector2(transform.localScale.x * rangeScale, transform.localScale.y * rangeScale);
        Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(currPoint, rangeSize, 0f, enemyLayers);

        foreach (Collider2D enemy in enemiesInRange)
        {
            Debug.Log(enemy.name);
            float Distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (Distance < shortestDistance)
            {
                nearestEnemy = enemy.transform;
                shortestDistance = Distance;
            }
        }
        Debug.Log(nearestEnemy.position);
        Debug.Log(Vector2.Angle(transform.position, nearestEnemy.position));
        Debug.Log(shortestDistance);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(currPoint, rangeSize);
    }
}
