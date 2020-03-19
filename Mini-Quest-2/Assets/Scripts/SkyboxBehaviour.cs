using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Troy Records Jr.
 * Last Updated: 2-23-2020
 * This script is responsible for hanlding
 * the Skybox animations. For this, it only
 * rotates around the world.
 */
public class SkyboxBehaviour : MonoBehaviour {

    public static SkyboxBehaviour instance = null;

    
	public float speed;     //This is used to calculate the speed at which the skybox rotates at

    // Use this for initialization
    void Start () {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);


        RenderSettings.skybox.SetFloat("_Rotation", 0); //Sets the skybox rotation to the default rotation
    }
	
	// Update is called once per frame
	void Update () {

        float value = RenderSettings.skybox.GetFloat("_Rotation") + (speed/10); //This the value to set the skybox rotation to. 

        RenderSettings.skybox.SetFloat("_Rotation", value); //Sets the skybox rotation to the calculated value

    }

    public void UpdateSpeed()
    {
        speed++;    //Increases the speed at which the skybox rotates at.
    }
}
