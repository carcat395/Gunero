using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum enemyType {Stationary, RandomMovement, FollowPlayer}

public class EnemyAI : MonoBehaviour
{
    public enemyType type;

    [Space]
    public Transform target;
    public Transform firingPoint;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Vector2 randomDirection = new Vector2(0, 0);

    bool isMoving;
    [SerializeField] bool isFacingRight;
    bool isShooting;
    float timer = 0;
    public float cooldownTime;
    public float moveCooldown;
    Vector2 direction = new Vector2();
    Vector3 currPos;
    Vector3 lastPos;

    BulletSpawner bulletspawner;
    Seeker seeker;
    Rigidbody2D rb;
    Enemy enemy;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        bulletspawner = GetComponent<BulletSpawner>();
        timer = cooldownTime;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && target != null)
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
            anim.SetBool("TargetFound", true);
        }
        else if(target == null)
        {
            anim.SetBool("TargetFound", false);
            Debug.Log("target not found :(");
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (type == enemyType.Stationary)
        {
            direction = ((Vector2)path.vectorPath[path.vectorPath.Count - 1] - rb.position).normalized;
            firingPoint.up = direction;

            if (timer <= 0)
            {
                bulletspawner.Shoot(isShooting = true, firingPoint);
                timer = cooldownTime;

            }
            else
            {
                bulletspawner.Shoot(isShooting = true, firingPoint);
                timer -= Time.deltaTime;
            }
        }
        else if(type == enemyType.FollowPlayer)
        {
            direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }
            
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
        else if(type == enemyType.RandomMovement)
        {
            direction = ((Vector2)path.vectorPath[path.vectorPath.Count] - rb.position).normalized;
            firingPoint.up = direction;

            if (timer <= 0 )
            {
                isMoving = false;
                bulletspawner.Shoot(isShooting = true, firingPoint);
                StartCoroutine(WaitToMove());
            }
            else
            {
                isMoving = true;
                Vector2 force = randomDirection * speed * Time.deltaTime;

                rb.AddForce(force);

                bulletspawner.Shoot(isShooting = false, firingPoint);
                timer -= Time.deltaTime;
            }

            lastPos = currPos;
        }

        if(direction.x < 0 && isFacingRight)
        {
            isFacingRight = false;
            Flip();
        }
        else if(direction.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
            Flip();
        }
    }

    public void Shoot()
    {
        direction = ((Vector2)path.vectorPath[path.vectorPath.Count - 1] - rb.position).normalized;
        firingPoint.up = direction;

        if (timer <= 0)
        {
            bulletspawner.Shoot(isShooting = true, firingPoint);
            timer = cooldownTime;

        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void SetIsMoving(bool value)
    {
        isMoving = value;
    }

    public void Move(Vector2 direction)
    {
        isMoving = true;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
    }

    public void MoveTowardsTarget()
    {
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    void Flip()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(.5f);
        randomDirection = Random.insideUnitCircle.normalized;
        Debug.Log("randomized direction : " + randomDirection);
        timer = moveCooldown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            timer = 0;
        }
        
        if(type == enemyType.FollowPlayer)
        {
            if(collision.collider.tag == "Player")
            {
                target.GetComponent<Player>().TakeDamage(target.GetComponent<Player>().MaxHP);
                enemy.TakeDamage(enemy.HP);
            }
        }
    }
}
