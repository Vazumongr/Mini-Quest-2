using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the general sounds throughout the game
 */
public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;
	public AudioSource audioSource;
	public AudioClip deathClip;	//Clip to play on player death
	public AudioClip musicClip;	//Clip for background music

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
			Destroy(this);

		DontDestroyOnLoad(this.gameObject);	//Allows the object to persist between main menu and level One
	}

	//THis simply utilizes coroutines to create a "fading out" effect of the background soundtrack
	public IEnumerator FadeOut()
	{
		while (audioSource.volume > 0)
		{
			audioSource.volume -= .01f;
			yield return null;
		}
		CueDeath();

	}

	//Cues the death clip
	void CueDeath()
	{
		audioSource.clip = deathClip;
		audioSource.volume = 1;
		audioSource.Play();
	}

	//Restarts the soundtrack
	public void RestartSoundtrack()
	{
		audioSource.clip = musicClip;
		audioSource.Play();
	}
}
