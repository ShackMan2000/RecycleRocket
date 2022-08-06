using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Hacker : MonoBehaviour
{

    // stuff like insta reload scene





    [SerializeField]
    private TextMeshProUGUI rocketSpeedText, distanceToGrabText;

    //[SerializeField]
    //private Rigidbody rocketRigidBody;







    //[SerializeField]
    //private RocketPhysics rocketPhysics;




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



    private void ReloadScene()
    {
        print("reload");
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }








}

