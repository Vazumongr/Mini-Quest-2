using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the sword, for both enemy and player.
 */
public class SwordController : MonoBehaviour {

	public AudioSource audioSource;	//So we can play our on hit sound

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if(transform.parent.CompareTag("Player"))       //Check if this is the player's sword
		{
			if (other.gameObject.CompareTag("Enemy"))	//Check if we are hit the enemy
			{
				SkyboxBehaviour.instance.UpdateSpeed();	//Further explained in 'SkyboxBehaviour'
				audioSource.Play();						//Plays on hit sfx
				other.GetComponent<Animator>().SetTrigger("EnemyDie");	//Tells enemy to die
			}
		}
		else if(transform.parent.CompareTag("Enemy"))   //Checks if this is the enemy's sword
		{
			if (other.gameObject.CompareTag("Player"))	//Check if they hit the player
			{
				other.GetComponent<PlayerControllerScript>().TakeDamage(10);	//Tells the player to take damage
			}
		}
        
    }
}
