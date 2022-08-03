using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackInput : MonoBehaviour
{





    //get the info from OVR Input
    //set the current thrust based on that
    //send out info as event to thrustcontroller


    //[SerializeField]
    //private OVRInput.Controller m_controller = OVRInput.Controller.None;



    [SerializeField]
    private Transform engine, fan;

    [SerializeField]
    private float minFanRotationSpeed, maxFanRotationSpeed;

    private float currentFanRotationSpeed;


   // private bool noThrustInput;


    private Vector3 currentForce;
    public Vector3 CurrentForce { get => currentForce; }


    [SerializeField]
    private float engineRotationSpeed;




    public event Action<Vector3> EvtThrustChanged = delegate { };
    public event Action<float> EvtThrustInputChanged = delegate { };


    //needs to send out actual force. or at least transform and thrust.
    //maybe just use one here. and the thrust controller multiplies it. Again Update about force changes, not the force


    private void Start()
    {
        ThrustInputChanged(0f);
    }



    private void Update()
    {
        //float newInput = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);

        //ThrustInputChanged(newInput);


        //if (OVRInput.Get(OVRInput.Button.One, m_controller))
        //    engine.Rotate(new Vector3(engineRotationSpeed * Time.deltaTime, 0f, 0f));

        //else if (OVRInput.Get(OVRInput.Button.Two, m_controller))
        //    engine.Rotate(new Vector3(-engineRotationSpeed * Time.deltaTime, 0f, 0f));

        //fan.Rotate(new Vector3(0f, 0f, currentFanRotationSpeed * Time.deltaTime));
    }

    public void ThrustInputChanged(float newInput)
    {
        //at 0 input this method still needs to run once to inform listeners (thrust/particles etc.)
        //if (newInput <= 0.01f)
        //    noThrustInput = true;

        currentForce = newInput * engine.forward;

     
        currentFanRotationSpeed = newInput * maxFanRotationSpeed + (1f - newInput) * minFanRotationSpeed;



        EvtThrustInputChanged(newInput);
        EvtThrustChanged(currentForce);
    }
}

