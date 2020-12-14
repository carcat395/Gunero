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

    [Space]
    public HealthBar healthbar;
    public GameObject gameManager;

    GameManager gm;

    // Start is called before the first frame update
    void Awake()
    {
        gm = gameManager.GetComponent<GameManager>();
        HP = MaxHP;
        healthbar.SetMaxHealth(MaxHP);
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
            healthbar.SetHealth(HP);
            recoveryTimer = recoveryTime;
            if (HP <= 0)
            {
                isDead = true;
                gm.SetGameOver();
            }
        }
    }
}
