using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int currGun;
    public int MaxHP;
    int HP;
    public float recoveryTime;
    float recoveryTimer;
    bool isDead;

    [Space]
    public HealthBar healthbar;
    public GameObject gameManager;

    GameManager gm;
    PlayerCombat pc;

    // Start is called before the first frame update
    void Awake()
    {
        pc = GetComponent<PlayerCombat>();
        gm = gameManager.GetComponent<GameManager>();
        HP = MaxHP;
        healthbar.SetMaxHealth(MaxHP);
    }

    private void Start()
    {
        pc.SetWeapon(gm.weaponIndex, gm.weaponProgress[gm.weaponIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        recoveryTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        healthbar.SetHealth(HP);
        recoveryTimer = recoveryTime;
        if (HP <= 0)
        {
            isDead = true;
            gm.SetGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy Bullet" && recoveryTimer <= 0)
        {
            TakeDamage(5);
        }
    }
}
