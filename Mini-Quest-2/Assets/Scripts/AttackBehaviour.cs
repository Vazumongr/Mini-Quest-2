using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the players attack behaviour. Simply set the state to idle when the attack is finished.
 */
public class AttackBehaviour : StateMachineBehaviour {

    private PlayerControllerScript playerController;    //Reference the player

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController = GameObject.Find("Player").GetComponent< PlayerControllerScript > ();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerController.SetState("idle");  //Set our state to idle when we finish an attack
    }
}
