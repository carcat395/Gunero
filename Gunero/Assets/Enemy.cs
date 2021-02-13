using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EnemyStatus {Normal, Poisoned}

public class Enemy : MonoBehaviour
{
    [Range(0, .3f)] [SerializeField] public float m_movementSmoothing = .05f;

    public string enemyID;
    public bool isBoss;
    public EnemyStatus status;

    public float maxHP;
    public float HP;
    Vector3 m_velocity = Vector3.zero;
    public int movementSpeed;
    Vector3 targetPosition;
    public int enemyWorth;

    public bool isMoving;
    bool isShooting = true;
    Vector3 currPos;
    Vector3 lastPos;
    public int rangeScale;
    Vector2 rangeSize;
    bool isDead;

    float poisonDamage;
    float timer = 0;
    
    public HealthBar healthbar;
    public TMP_Text damageTaken;
    public Transform firingPoint;
    public LayerMask enemyLayers;

    Animator anim;

    void Awake()
    {
        if (isBoss)
        {
            anim = GetComponentInChildren<Animator>();
        }
        else
        {
            anim = GetComponent<Animator>();
        }
        HP = maxHP;
        healthbar.SetMaxHealth(maxHP);
        status = EnemyStatus.Normal;
    }

    private void OnEnable()
    {
        if(!isBoss)
            anim.SetBool("Liver", true);
        healthbar.SetMaxHealth(maxHP);
        HP = maxHP;
    }

    private void Update()
    {
        if(status == EnemyStatus.Poisoned && timer >= 0)
        {
            StartCoroutine(TakePoisonDamage());
        }
        else if(timer == 0)
        {
            status = EnemyStatus.Normal;
        }
    }

    public void TakeDamage(float damage)
    {
        damageTaken.text = "-" + damage.ToString();
        anim.SetTrigger("Hurt");
        HP -= damage;
        healthbar.SetHealth(HP);
        if (HP <= 0)
        {
            //GetComponentInParent<Room>().DecreaseEnemyCount();
            Debug.Log("Gained :" + enemyWorth + "parts");
            GameManager.AddToParts(enemyWorth);
            anim.SetBool("Liver", false);
            //gameObject.SetActive(false);
        }
    }

    IEnumerator TakePoisonDamage()
    {
        Debug.Log("HEY, this is a war crime !!!");
        TakeDamage(poisonDamage);
        yield return new WaitForSeconds(.5f);
        timer -= .5f;
    }

    public void SetPoison(float damageOverTime, float statusTime)
    {
        if (status != EnemyStatus.Poisoned)
        {
            status = EnemyStatus.Poisoned;
            poisonDamage = damageOverTime;
            timer = statusTime;
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player BUllet")
        {
            Bullet hitBullet = collision.GetComponent<Bullet>();
            if (hitBullet.bulletType == BulletType.Normal) {
                int damageTaken = collision.GetComponent<Bullet>().damage;
                TakeDamage(damageTaken);
            }
        }
    }
}
