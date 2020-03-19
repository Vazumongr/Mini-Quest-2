using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * metadata of the enemies For this project,
 * it simply holds the patrolRange and 
 * the Destroy method.
 */
public class EnemyMetadata : MonoBehaviour {

	/*
	 * So this variable is designed to specify specific pattrolRanges for each enemy.
	 * This value is editted in the inspector with each enemy placement so that
	 * each enemy has the appropriate patrolRange for their location.
	 * A patrolrange of 0 is meant to represent the default value and the 
	 * EnemyWalkBehaviour will recognize that as a default and use a preset patrolRange.
	 */
	public float patrolRange = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DestroyMe()
	{
		Destroy(this.gameObject);
	}
}
