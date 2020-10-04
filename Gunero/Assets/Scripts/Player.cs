using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int MaxHP;
    int HP;
    public float recoveryTime;
    float recoveryTimer;

    // Start is called before the first frame update
    void Start()
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
        if(collision.tag == "Bullet" && recoveryTimer <= 0)
        {
            HP -= 1;
            print(HP);
            recoveryTimer = recoveryTime;
        }
    }
}
