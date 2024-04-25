using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimations : StateMachineBehaviour
{
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetInteger("idleIndex", Random.Range(0, 4));
    }
}
