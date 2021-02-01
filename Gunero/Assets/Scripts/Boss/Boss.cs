using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss1State {Idle, SpeenShot, WideAreaShot, ApproachingPlayer}

public class Boss : MonoBehaviour
{
    public Boss1State bossState;

    bool Phase2;
    int maxHP;
    public int currHP;

    public float rotation;
    float startingRotation;
    public float seconds;
    float t = 0;

    public GameObject target;
    public GameObject shootingPoint;

    void Start()
    {
        
    }
    
    void Update()
    {
        if(bossState == Boss1State.Idle)
        {
            return;
        }
        else if (bossState == Boss1State.SpeenShot)
        {
            t += Time.deltaTime;
            if (t >= seconds)
            {
                t = 0;
                startingRotation = transform.eulerAngles.z;
            };
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(startingRotation, startingRotation + rotation, t / seconds));
        }
    }
}
