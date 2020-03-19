using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the UI on the main menu.
 */
public class UIMenuManager : MonoBehaviour {


	[SerializeField]
	private Text _menuText;

	public static UIMenuManager instance;
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	public void UpdateMenuText()
	{
		_menuText.text = "You need to use A and D to move left and right respectively. " +
			"\nYou need to use W to jump and spacebar to attack.";
	}
}
