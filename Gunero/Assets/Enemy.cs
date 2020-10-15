using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] public float m_movementSmoothing = .05f;
    public int maxHP;
    int HP;
    Vector3 m_velocity = Vector3.zero;
    public int movementSpeed;
    public float moveTimer;
    float timer;
    Vector3 targetPosition;
    public bool isMoving;
    Rigidbody2D m_rigidbody2D;
    BulletSpawner bulletspawner;
    bool isShooting = true;
    Vector3 currPos;
    Vector3 lastPos;
    bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        bulletspawner = GetComponent<BulletSpawner>();
        timer = moveTimer;
        isMoving = false;
        HP = maxHP;
    }

    private void Start()
    {
        getTarget();
    }
    // Update is called once per frame
    private void Update()
    {
       currPos = transform.position;
       if (timer <= 0)
        {
            if (!isMoving)
            {
                getTarget();
                isMoving = true;
                bulletspawner.Shoot(isShooting = false);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player BUllet")
        {
            HP -= 5;
            Debug.Log("Enemy HP:" + HP);
            if (HP <= 0)
            {
                Debug.Log("Enemy Dead");
                isDead = true;
                Time.timeScale = 0f;
            }
        }
    }

    void OnGUI()
    {
        if (isDead)
            GUI.Box(new Rect(0, 0, 400F, 600f), "Enemy Dead");
    }
}
