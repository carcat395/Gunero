using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_CollsionDetection : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Obstacle")
        {
            anim.SetBool("ChasePlayer", false);
        }
    }
}
