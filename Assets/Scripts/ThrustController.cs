using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrustController : MonoBehaviour
{

    private Vector3 currentForce;

    [SerializeField]
    private Text speedText;


    public float speedMagnitude => Vector3.Magnitude(velocity);

    [SerializeField]
    private JetPackInput jetPack01, jetPack02;


    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private float maxVelocity;


    [SerializeField]
    private float addForceMulti = 1f;


    private Vector3 velocity;


    private void OnEnable()
    {
        jetPack01.EvtThrustChanged += ChangeForce;
        jetPack02.EvtThrustChanged += ChangeForce;
    }


   
    
    public void ChangeForce(Vector3 newForce)
    {
        currentForce = (jetPack01.CurrentForce  + jetPack02.CurrentForce) / 2f * addForceMulti;      
    }


    public Vector3 TestVelocity;

        

    private void FixedUpdate()
    {
        rigidBody.AddForce(currentForce);

        velocity = rigidBody.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));

        speedText.text = Vector3.Magnitude(velocity).ToString("F1") + " / " + Vector3.Magnitude(new Vector3(maxVelocity, maxVelocity, maxVelocity)).ToString("F1");
           

        rigidBody.velocity = velocity;
        TestVelocity = velocity;

    }



    private void OnDisable()
    {
        jetPack01.EvtThrustChanged -= ChangeForce;
        jetPack02.EvtThrustChanged -= ChangeForce;
    }




}

