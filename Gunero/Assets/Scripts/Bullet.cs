using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public bool enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
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
                Destroy(gameObject);
            }
        }
    }
}
