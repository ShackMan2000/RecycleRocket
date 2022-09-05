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

    public ExplosionManager failedExplosion;

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


        // rocketSpeedText.text = rocketRigidBody.velocity.ToString("F1") + " avg:" + rocketPhysics.GetAbsoluteFallSpeed().ToString("F1");
    }





    //private void OnValidate()
    //{
    //        launchThrust.MoveWithHacker(launchSlider);
    //        landingBurn.MoveWithHacker(landingBurnSlider);

    //    xRotation.MoveWithHacker(xRotationSlider);
    //    yRotation.MoveWithHacker(yRotationSlider);
    //}



    [ContextMenu("fail")]
    public void Faill()
    {
        failedExplosion.CreateExplosion(Vector3.zero);
    }


    [ContextMenu("debug")]
    public void Debugll()
    {
        Debug.Log("aaaaaaaaaaaaaaaaasdfdfgadfgggggggafdsgadsfgsdfgsdfgsdfg");
    }


    private void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }








}

