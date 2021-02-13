using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Boss1State {Idle, SpeenShot, WideAreaShot, ApproachingPlayer}

public class Boss : MonoBehaviour
{
    public Boss1State nextState;
    Boss1State lastState;

    bool Phase2;
    public int maxHP;
    int currHP;

    public int speed;

    public GameObject target;
    public GameObject shootingPoint;
    public GameObject vCam;

    void Start()
    {
        nextState = Boss1State.ApproachingPlayer;
    }
    
    void Update()
    {
        
    }

    public Boss1State DetermineNextState()
    {
        Debug.Log("distance to player : " + Vector3.Distance(transform.position, target.transform.position));
        if (Vector3.Distance(transform.position, target.transform.position) <= 2 && nextState != Boss1State.WideAreaShot)
        {
            lastState = nextState;
            nextState = Boss1State.WideAreaShot;
        }
        else
        {
            if (nextState == Boss1State.SpeenShot || lastState == Boss1State.SpeenShot)
            {
                nextState = Boss1State.ApproachingPlayer;
            }
            else if (nextState == Boss1State.ApproachingPlayer || lastState == Boss1State.ApproachingPlayer)
            {
                nextState = Boss1State.SpeenShot;
            }
        }

        return nextState;
    }
}
