using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject BulletResource;
    public GameObject AttackRange;

    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
    public float angle;
    public int rangeScale;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    private void Awake()
    {
        timer = cooldown;
        
    }

    // Update is called once per frame
    void Update()
    {
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
        b.rotation = angle;
        b.speed = bulletSpeed;
        b.velocity = bulletVelocity;
        
        return spawnedBullet;
    }

    void autoAim()
    {
        Vector2 currPoint = transform.position;
        Vector2 rangeSize = new Vector2(transform.localScale.x * rangeScale, transform.localScale.y * rangeScale);
        Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(currPoint, rangeSize, enemyLayers);
        foreach (Collider2D enemy in enemiesInRange)
        {
            /*
            Enemy[] enemiesInRange = GetEnemiesInRange();
            Enemy nearestEnemy = null;
            float bestAngle = -1f;
            foreach (Enemy enemy in enemiesInRange)
            {
                Vector3 vectorToEnemy = enemy.transform.position - transform.position;
                vectorToEnemy.normalize();
                float angleToEnemy = Vector3.Dot(transform.forward, vectorToEnemy);
                if (angleToEnemy > bestAngle)
                {
                    nearestEnemy = enemy;
                    bestAngle = angleToEnemy;
                }
            }*/
        }
    }
}
