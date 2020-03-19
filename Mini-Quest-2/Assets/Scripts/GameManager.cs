using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the game. Global events that need to be
 * handled are handled by the GameManager 
 * such as Ending the game and resetting the game.
 */
public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private int _returnCount = 0;	//keep track of how many times user has pressed enter

	private bool _playerAlive = false;	//Keep track of if the player is alive

	void Awake()
	{
		if(instance==null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
		if(SceneManager.GetActiveScene().name.Equals("Menu"))	//If we are at the main menu
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if (_returnCount > 0)	//User has pressed enter before...
					SceneManager.LoadScene("LevelOne");	//Load the game
				else	//Users first time pressing enter...
				{
					UIMenuManager.instance.UpdateMenuText();	//Update text to display controls
					_returnCount++;
				}
			}
		}
		else if(SceneManager.GetActiveScene().name.Equals("LevelOne"))	//If we are at LevelOne
		{
			if(!_playerAlive)	//Player has died...
			{
				if (Input.GetKeyDown(KeyCode.R))
				{
					ResetGame();
				}
			}
		}
	}

    public void ResetGame()
    {
		SoundManager.instance.RestartSoundtrack();
        SceneManager.LoadScene("Menu");
    }
	/*
	 * This method is so the GameManager can
	 * do what is necessary upon the player dying,
	 * such as Destroying the enemies, fading out music,
	 * displaying death text, etc.
	 */
	public void PlayerDied()
	{
		_playerAlive = false;	//Players dead
		SoundManager.instance.StartCoroutine("FadeOut");	//Starts fading out the music
		UIManager.instance.PlayerDied();	//Tells the UIManager the player has died
		GameObject enemyContainer = GameObject.Find("EnemyContainer");	//EnemyContainer that holds all enemies
		foreach(Transform child in enemyContainer.transform)	//Cycles through the containers children
		{
			child.GetComponent<Animator>().SetTrigger("EnemyDie");	//Tells each enemy to die
		}
	}

	/*
	 * This is the GameOver scenario in case the player
	 * makes it to the end.
	 */
	public void GameOver()
	{
		SceneManager.LoadScene("EndScene");
	}

	public void PlayerSpawned()
	{
		_playerAlive = true;
	}
}
