using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the UI on LevelOne. It is simply named UIManager 
 * because I didn't have a mainmenu at the time
 * I created this.
 */
public class UIManager : MonoBehaviour {

	public static UIManager instance;

	[SerializeField]
	private Slider _playerHealthBar;
	[SerializeField]
	private Text  _resetGameText,_playerDiedText;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	//Updates the player health bar to reflect player health
	public void UpdatePlayerHealthBar(float hp)
	{
		_playerHealthBar.value = hp / 100;
	}
	
	//Todo when the player dies
	public void PlayerDied()
	{
		_playerDiedText.fontSize = 1;	//Set died text really small
		_playerDiedText.text = "YOU HAVE DIED";	//Set died text message
		_resetGameText.text = "Press 'R' to restart reality";	//Display control for restarting the game
		StartCoroutine(DiedTextEnlarge());	//Start the died text animation
	}

	/*
	 * This replicates a text enlarging animation 
	 * so the text grows over time. Inspired by the
	 * soulsborne series.
	 */
	IEnumerator DiedTextEnlarge()
	{
		while(_playerDiedText.fontSize < 95)
		{
			_playerDiedText.fontSize++;
			yield return null;
			yield return null;
		}
	}
}
