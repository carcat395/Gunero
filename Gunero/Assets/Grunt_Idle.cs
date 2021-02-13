using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Idle : StateMachineBehaviour
{
    public float timer;
    float t = 0;
    bool currState;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t = 0;

        currState = animator.GetComponent<EnemyAI>().firing;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t += Time.deltaTime;
        if (t >= timer)
        {
            if (currState)
            {
                animator.SetBool("isMoving", true);
                currState = false;
            }
            else
            {
                animator.SetTrigger("Attack");
                currState = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.ResetTrigger("Attack");
    }
}
