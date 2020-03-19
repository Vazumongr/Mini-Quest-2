using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the player. This handles everything
 * for the player such as movement, animation,
 * and attacking.
 */
public class PlayerControllerScript : MonoBehaviour {

	public Rigidbody2D playerRigidBody;	//Player rigidbody
	public Animator playerAnimator;	//Player animator
	public Animator swordCollider;	//The collider of the sword
	private string _currentState;	//The players current state
	private float _direction;	//Direction the player is facing
	public float distToGround;	//The distance to the ground

	public LayerMask groundLayers;	//LayerMask to raycast against for grounding

	[SerializeField]
	private float _speed,_jumpSpeed = .2f;	//Movement speed and jump speed respectively

	private List<string> _attackableStates;	//List of states the player can attack from

	private int _health;	//Players health

	// Use this for initialization
	void Start () {
		_currentState = "idle";	//Default to idle
		_attackableStates = new List<string>() { "walking", "idle" };	//List our attacking states
		_health = 100;	//Set our health
		GameManager.instance.PlayerSpawned();	//Tell gameManager we have spawned
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space) && _attackableStates.Contains(_currentState) && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack01")) //If we are not currently attacking and in an attackable state.
		{
			_currentState = "attacking";	//Set our state to attacking
		}

		AdjustAnimation();	//Adjust the animation based on our state
	}

	void FixedUpdate()
	{
		CalculateMovement();

	}

	void CalculateMovement()
	{
		if(IsGrounded())
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				playerRigidBody.AddForce(Vector2.up*_jumpSpeed,ForceMode2D.Impulse);	//Our jump
				//_currentState = "inAir";	//This doesn't get used.
			}
		}
		if (Input.GetKey(KeyCode.A))
		{
			_direction = -1;
			transform.localScale = new Vector3(-1, 1, 1);	//Flips the sprite to face our direction
			playerRigidBody.velocity = new Vector2(1 * _direction * _speed, playerRigidBody.velocity.y);	//Move in that direction. I didn't use addForce because I didn't like the way it moved.
			_currentState = "walking";
		}
		else if (Input.GetKey(KeyCode.D))
		{
			_direction = 1;
			transform.localScale = new Vector3(1, 1, 1);
			playerRigidBody.velocity = new Vector2(1 * _direction * _speed, playerRigidBody.velocity.y);	//I don't like the sliding around. Directly adjusting Velocity is the easier way to work around it.
			_currentState = "walking";
		}
		else
		{
			_direction = 0;
			playerRigidBody.velocity = new Vector2(1 * _direction * _speed, playerRigidBody.velocity.y);	//Don't want to move if the player isn't trying to move.
			_currentState = "idle";
		}

	}

	/*
	 * This handles changing our animation of the
	 * player. This is a sloppy make up of a poorly
	 * designed state machine. It works but this whole
	 * PlayerControllerScript could be rewritten
	 * better.
	 */
	void AdjustAnimation()
	{
		switch(_currentState)
		{
			case "idle":
				ResetAnimations();
				playerAnimator.SetBool("isIdle", true);
                break;
			case "walking":
				ResetAnimations();
				playerAnimator.SetBool("isWalking", true);
				break;
			case "attacking":
				ResetAnimations();
				playerAnimator.SetBool("isAttacking", true);
				playerAnimator.SetTrigger("Attack");
                break;
			case "dead":
				ResetAnimations();
				playerAnimator.SetTrigger("Dead");
				break;
			case "inAir":
				ResetAnimations();
				playerAnimator.SetBool("inAir",true);
				break;
			default:
				Debug.LogError("Animation not recognized");
				break;
		}
	}

	//Resets all our bool parameters in the animator
	void ResetAnimations()
	{
		playerAnimator.SetBool("isWalking", false);
		playerAnimator.SetBool("isIdle", false);
		playerAnimator.SetBool("isCrouch", false);
		playerAnimator.SetBool("isDizzy", false);
		playerAnimator.SetBool("isAttacking", false);
		playerAnimator.SetBool("inAir", false);
    }

	//Sets player state
    public void SetState(string state)
    {
		ResetAnimations();
        _currentState = state;
    }

	//Are we grounded?
	bool IsGrounded()
	{
		Debug.DrawRay(transform.position, Vector2.down * distToGround,Color.green);
		return Physics2D.Raycast(transform.position, Vector2.down, distToGround,groundLayers);
	}


	//Goes through the process of the player taking damage
	public void TakeDamage(int dmgValue)
	{
		_health -= dmgValue;	//Reduce health
		UIManager.instance.UpdatePlayerHealthBar(_health);	//update the healthbar

		if(_health <= 0)	//If we are at or below 0 health
		{
			Die();	//We die.

		}
	}

	//This handles the player dying
	private void Die()
	{
		GameManager.instance.PlayerDied();	//Tell the gamaManager we have died
		_currentState = "dead";	//Set our state to dead
	}

	//Called to destroy this object. This gets used in the animator event system.
	public void DestroyMe()
	{
		Destroy(this.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("EndZone"))	//If we made it to the end
		{
			GameManager.instance.GameOver();	//Tell the gameManager it's game over.
		}
	}

	
}
