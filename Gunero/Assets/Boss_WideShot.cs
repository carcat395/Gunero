using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_WideShot : StateMachineBehaviour
{
    Vector2 direction;

    BulletSpawner bs;
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bs = animator.GetComponent<BulletSpawner>();
        rb = animator.GetComponentInParent<Rigidbody2D>();

        bs.ChangeIndex(1);

        direction = (animator.GetComponent<Boss>().target.transform.position - rb.transform.position).normalized;
        animator.GetComponent<Boss>().shootingPoint.transform.up = direction;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("index: " + bs.index);
        bs.Shoot(true, animator.GetComponent<Boss>().shootingPoint.transform);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bs.Shoot(false, animator.GetComponent<Boss>().shootingPoint.transform);
        bs.ChangeIndex(0);

        animator.ResetTrigger("WideShot");
    }
}
