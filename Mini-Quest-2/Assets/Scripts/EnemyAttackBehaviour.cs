using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the enemys attack behaviour.
 */
public class EnemyAttackBehaviour : StateMachineBehaviour {
	private Rigidbody2D _rb;
	

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_rb = animator.GetComponent<Rigidbody2D>();	//Gets the enemys rigidbody
		_rb.constraints = RigidbodyConstraints2D.FreezeAll;	//Stops all movement
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
			
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("EnemyAttack");	//Resets the trigger to prevent the animation from playing twice consecutively
		_rb.constraints = RigidbodyConstraints2D.FreezeRotation;	//Resumes normal movement
	}
}
