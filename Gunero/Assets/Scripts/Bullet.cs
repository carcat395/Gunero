using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float lifetime;
    float timer;
    public bool enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        timer = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0) gameObject.SetActive(false);
    }

    public void ResetTimer()
    {
        timer = lifetime; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle" || collision.tag == "Player" || collision.tag == "Enemy")
        {
            if (enemyBullet && collision.tag == "Enemy")
            {
                return;
            }
            else if (!enemyBullet && collision.tag == "Player")
            {
                return;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
