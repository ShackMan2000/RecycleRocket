using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Hacker : MonoBehaviour
{

    // stuff like insta reload scene





    [SerializeField]
    private TextMeshProUGUI distanceToGrabText;

    //[SerializeField]
    //private Rigidbody rocketRigidBody;





    [SerializeField]
    private HandGrabber handGrabber;


    //[SerializeField]
    //private RocketPhysics rocketPhysics;

    [SerializeField]
    private SliderControl launchThrust, landingBurn, xRotation, yRotation;


    [Range(0f, 1f)]
    public float launchSlider, landingBurnSlider;

    [Range(-1f, 1f)]
    public float xRotationSlider, yRotationSlider;


    //[SerializeField]
    //private LevelManager levelMan;


    private void OnEnable()
    {
        print("WARNING, hacker is in the scene");


    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ReloadScene();

        distanceToGrabText.text = handGrabber.currentDistanceHandToAnchor.ToString("F2");

        // rocketSpeedText.text = rocketRigidBody.velocity.ToString("F1") + " avg:" + rocketPhysics.GetAbsoluteFallSpeed().ToString("F1");
    }





    private void OnValidate()
    {
            launchThrust.MoveWithHacker(launchSlider);
            landingBurn.MoveWithHacker(landingBurnSlider);

        xRotation.MoveWithHacker(xRotationSlider);
        yRotation.MoveWithHacker(yRotationSlider);
    }

    private void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }








}

