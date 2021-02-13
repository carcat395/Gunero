using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : StateMachineBehaviour
{
    public float rotation;
    float startingRotation;
    public float seconds;
    float t = 0;
    public int maxRotations;
    int rotations;

    BulletSpawner bs;
    public GameObject shootingPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bs = animator.GetComponent<BulletSpawner>();
        //shootingPoint = animator.GetComponent<Boss>().shootingPoint;
        rotations = 0;

        bs.Shoot(true, animator.GetComponent<Boss>().shootingPoint.transform);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        t += Time.deltaTime;
        if (t >= seconds)
        {
            t = 0;
            startingRotation = animator.GetComponent<Boss>().shootingPoint.transform.eulerAngles.z;
            rotations++;

            if(rotations == maxRotations)
                animator.SetBool("SpinningAttack", false);
        }
        animator.GetComponent<Boss>().shootingPoint.transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(startingRotation, startingRotation + rotation, t / seconds));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bs.Shoot(false, animator.GetComponent<Boss>().shootingPoint.transform);
    }
}
