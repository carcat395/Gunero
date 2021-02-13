using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt_Move : StateMachineBehaviour
{
    Vector2 targetPosition;
    float speed;
    public int radius;

    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPosition = Random.insideUnitCircle.normalized * radius;

        rb = animator.GetComponent<Rigidbody2D>();
        speed = animator.GetComponent<EnemyAI>().speed;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, targetPosition) <= 0.5)
        {
            animator.SetBool("ChasePlayer", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
}
