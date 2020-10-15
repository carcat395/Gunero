using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int MaxHP;
    int HP;
    public float recoveryTime;
    float recoveryTimer;
    bool isDead;

    // Start is called before the first frame update
    void Awake()
    {
        HP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        recoveryTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy Bullet" && recoveryTimer <= 0)
        {
            HP -= 5;
            Debug.Log("Player HP:" + HP);
            recoveryTimer = recoveryTime;
            if (HP <= 0)
            {
                Debug.Log("Player ded");
                isDead = true;
                Time.timeScale = 0f;
            }
        }
    }

    void OnGUI()
    {
        if(isDead)
        GUI.Box(new Rect(0, 0, 400F, 600f), "Player Dead");
    }
}
