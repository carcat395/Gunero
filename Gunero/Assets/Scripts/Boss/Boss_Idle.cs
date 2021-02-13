using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
{
    public float timer;
    float t = 0;

    Boss boss;
    Boss1State nextState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t = 0;

       nextState = animator.GetComponent<Boss>().DetermineNextState();
        Debug.Log("nextState : " + nextState);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (nextState == Boss1State.WideAreaShot)
        {
            animator.SetTrigger("WideShot");
        }
        else
        {
            t += Time.deltaTime;
            if (t >= timer)
            {
                if(nextState == Boss1State.ApproachingPlayer)
                {
                    animator.SetBool("ChasePlayer", true);
                }
                else if(nextState == Boss1State.SpeenShot)
                {
                    animator.SetBool("SpinningAttack", true);
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
