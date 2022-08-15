using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{

    //level 1 launch and land

    // - Welcome to the rocket recycling training center!
    // - Launch the rocket to an altitude of 100feet!

    // hide hologram and rotation sliders
    // disable landing burn
    // don't disable rocket when colliding, maybe some kind of landing mode or bool that it has been launched somewhat
    // check for rocket Height, when reached disable launch slider, slow it down automatically, no longer grabbable
    // enable landing gear button

    [SerializeField]
    private TextMeshProUGUI instructionText;


    [SerializeField]
    private SliderControl launchThrust, landingBurn, xRotation, yRotation;


    [SerializeField]
    private GameObject hologram;



    //for now just treat every step like a level, later can break down in level and steps
    public int currentLevel;


    [SerializeField]
    private float rocketHeightLevel01, rocketHeightCountAsLaunched;


    [SerializeField]
    private Rigidbody rocketRigidbody;


    [SerializeField]
    private RocketPhysics rocketPhysics;


    [SerializeField]
    private float maxRandomRotation;






    private void Awake()
    {
        currentLevel = 0;

        xRotation.gameObject.SetActive(false);
        yRotation.gameObject.SetActive(false);
        hologram.SetActive(false);


        instructionText.text = "Launch rocket";
    }



    //public void GiveRocketRandomRotation()
    //{
    //    Vector3 newRotation = new Vector3(Random.Range(-maxRandomRotation, maxRandomRotation), Random.Range(-maxRandomRotation, maxRandomRotation), Random.Range(-maxRandomRotation, maxRandomRotation));


    //    rocketRigidbody.transform.Rotate(newRotation);
    //}



    //level 0 Launch rocket to certain height


    private void Update()
    {
        if (currentLevel == 0)
        {
            //check for rocket Height          

            //disable launch slider and shut it down, might have to tell the hand to let go. that would be a good functionality for handy stuff


            if (!rocketPhysics.hasBeenLaunched && rocketPhysics.transform.position.y > rocketHeightCountAsLaunched)
            {
                rocketPhysics.hasBeenLaunched = true;
            }

            if (rocketRigidbody.transform.position.y > rocketHeightLevel01)
            {    // show text that engine shuts down
                instructionText.text = "Reached height! Engine shutting down";
            }



        }
        else if (currentLevel == 1)
        {



        }
        else if (currentLevel == 2)
        {



        }







        //if (Input.GetKey(KeyCode.R))
        //    GiveRocketRandomRotation();
    }

}
