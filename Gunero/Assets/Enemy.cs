using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] public float m_movementSmoothing = .05f;

    public string enemyID;

    public int maxHP;
    public int HP;
    Vector3 m_velocity = Vector3.zero;
    public int movementSpeed;
    public float moveTimer;
    Vector3 targetPosition;
    public int enemyWorth;

    public bool isMoving;
    bool isShooting = true;
    Vector3 currPos;
    Vector3 lastPos;
    bool isDead;
    float timer;

    Rigidbody2D m_rigidbody2D;
    BulletSpawner bulletspawner;
    public HealthBar healthbar;
    
    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        bulletspawner = GetComponent<BulletSpawner>();
        timer = moveTimer;
        isMoving = false;
        HP = maxHP;
        healthbar.SetMaxHealth(maxHP);
    }

    private void OnEnable()
    {
        healthbar.SetMaxHealth(maxHP);
        HP = maxHP;
    }

    private void Start()
    {
        getTarget();
    }

    private void Update()
    {
       currPos = transform.position;
       if (timer <= 0)
        {
            if (!isMoving)
            {
                getTarget();
                isMoving = true;
                bulletspawner.Shoot(isShooting = true);
                Move(targetPosition, movementSpeed);
            }
            else if (currPos != lastPos)
            {
                Move(targetPosition, movementSpeed);
            }
            else
            {
                timer = moveTimer;
                isMoving = false;
            }

        }
        else
        {
            bulletspawner.Shoot(isShooting = true);
            timer -= Time.deltaTime;
        }

        lastPos = currPos;
    }

    void getTarget()
    {
        targetPosition = new Vector3(Random.Range(-2.13f, 2.14f), Random.Range(0.78f, 4.69f), transform.position.z);
    }

    private void Move(Vector2 targetPos, float Speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    private void TakeDamage(int damage)
    {
        HP -= damage;
        healthbar.SetHealth(HP);
        if (HP <= 0)
        {
            GetComponentInParent<Room>().DecreaseEnemyCount();
            Debug.Log("Gained :" + enemyWorth + "parts");
            GameManager.AddToParts(enemyWorth);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player BUllet")
        {
            int damageTaken = collision.GetComponent<Bullet>().damage;
            TakeDamage(damageTaken);
        }
    }
}
