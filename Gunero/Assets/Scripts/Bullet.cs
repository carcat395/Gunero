using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Normal, AOE, Poison };

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float lifetime;
    float timer;
    public int damage;
    public bool enemyBullet = false;
    public LayerMask enemyLayers;
    public int explosionScale;
    public BulletType bulletType;
    Animator anim;
    IEnumerator coroutine;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        timer = lifetime;
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        timer = lifetime;
    }
    
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (bulletType == BulletType.AOE)
            {
                coroutine = Explosion();
                StartCoroutine(coroutine);
            }
            else if (bulletType == BulletType.Poison)
            {
                coroutine = PoisonExplosion();
                StartCoroutine(coroutine);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer()
    {
        timer = lifetime; 
    }

    void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionScale, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        Physics2D.OverlapBoxAll(transform.position, new Vector2(2, 3), enemyLayers);
    }

    IEnumerator Explosion()
    {
        speed = 0;
        anim.SetTrigger("Explode");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionScale, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.tag == "Enemy" || enemy.tag == "Boss")
                enemy.GetComponent<Enemy>().TakeDamage(damage);
            else if (enemy.tag == "Player")
                enemy.GetComponent<Player>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(0.20f);
        gameObject.SetActive(false);
    }

    IEnumerator PoisonExplosion()
    {
        speed = 0;
        anim.SetTrigger("Explode");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionScale, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<Enemy>().TakeDamage(damage);
            enemy.GetComponent<Enemy>().SetPoison(1, 5);
        }
        yield return new WaitForSeconds(0.20f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" || collision.tag == "Player" || collision.tag == "Enemy" || collision.tag == "Boss")
        {
            if (enemyBullet && (collision.tag == "Enemy" || collision.tag == "Boss"))
            {
                return;
            }
            else if (!enemyBullet && collision.tag == "Player")
            {
                return;
            }
            else
            {
                if (bulletType == BulletType.AOE)
                {
                    coroutine = Explosion();
                    StartCoroutine(coroutine);
                }
                else if(bulletType == BulletType.Poison)
                {
                    coroutine = PoisonExplosion();
                    StartCoroutine(coroutine);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
