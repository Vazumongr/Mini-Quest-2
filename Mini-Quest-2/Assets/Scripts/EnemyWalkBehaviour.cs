using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the enemies walking behaviour. This controls patrolling
 * and initiating the attack.
 */
public class EnemyWalkBehaviour : StateMachineBehaviour {
	private Transform _player;	//Hold player transform
	private Rigidbody2D _rb;	//Hold this enemies Rigidbody2D
	private EnemyMetadata _metadata;	//Hold enemy metadata (it's really just a C# script holding variables)

	private Vector2 _patrolPointA;	//The patrol point enemies will approach by default and is the right
	private Vector2 _patrolPointB;	//patrol point to the left
	private Vector2 _patrolRange;	//Range that they patrol

	private float _chaseRange;		//Range the player has to be in to be chased
	private float _attackRange;		//Range the player has to be in to be attacked

	private bool _approachingA = true;	//used to keep track of where the enemy is heading

    private bool _spawnSet;	//Track if we recorded the enemy spawn location

    private Vector2 _spawnLocation;	//Used to hold the enemy spawn location

	[SerializeField]
	private float _speed;	//Speed at which the enemy moves.
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		_player = GameObject.Find("Player").GetComponent<Transform>();	//Player transform
		_rb = animator.GetComponent<Rigidbody2D>();	//Rigidbody of this enemy
		_metadata = animator.GetComponent<EnemyMetadata>();	//Metadata of this enemy

        if (!_spawnSet)	//If this enemies spawn location has not been recorded
        {
            _spawnLocation = _rb.position;	//Record the enemies spawn location
            _spawnSet = true;	//So we don't do it again

        }

        _chaseRange = 10;	//How close the player must get before the enemy chases
		_attackRange = 2;	//How close the player must get before the enemy attacks

		if(_metadata.patrolRange != 0)	//If the patrolRange is not the default
		{
			_patrolRange = new Vector2(_metadata.patrolRange, 0);	//Set the patrolRange to what is in the metadata
		}
		else
		{
			_patrolRange = new Vector2(5, 0);	//The "default" patrolRange
		}

		

		_patrolPointA = _spawnLocation + _patrolRange;	//Calculate patrolPointA
		_patrolPointB = _spawnLocation - _patrolRange;	//Calculate patrolPointB
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Vector2 target = _patrolPointA;	//Default target

		float distance = Mathf.Abs(_rb.transform.position.x - _player.transform.position.x);	//Calculate distance between player and enemy

		if (distance < _chaseRange)	//If the player is within chasing range
		{
			target = new Vector2(_player.transform.position.x, _rb.transform.position.y);	//Set them as the target
			if(distance < _attackRange)	//If the player is within attacking range
			{
				animator.SetTrigger("EnemyAttack");	//Attack
			}
		}
		else	//Player is not within attacking range
		{
			if (_approachingA)	//If we need to approach patrolPointA
			{
				target = _patrolPointA;	//Set patrolPointA as target
			}
			else
			{
				target = _patrolPointB;	//Set patrolPointB as target
			}
		}


		_rb.transform.localScale = new Vector3(1* Mathf.Sign(target.x - _rb.position.x), 1, 1);	//Flips the sprite based off which direction we are heading

		Vector2 newPos = Vector2.MoveTowards(_rb.position, target, _speed * Time.fixedDeltaTime);	//Calculates the position to move to approach our target
		_rb.MovePosition(newPos);	//Move to our calculated position

		if(_rb.position.x >= _patrolPointA.x)	//If we have passed patrolPointA
		{
			_approachingA = false;	//We need to start going towards B
		}
		else if (_rb.position.x <= _patrolPointB.x) //If we have passed patrolPointB
		{
			_approachingA = true;   //We need to start going towards A
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}
}
