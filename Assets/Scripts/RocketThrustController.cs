using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketThrustController : MonoBehaviour
{


    [SerializeField]
    private InputActionAsset playerControls;

    private InputAction pressA, pressB;



    //private float thrustSliderValue;

    //public float ThrustSliderValue
    //{
    //    get => thrustSliderValue;

    //    set
    //    {
    //        thrustSliderValue = Mathf.Clamp(value, 0f, 1f);
    //        ThrustInputChanged(thrustSliderValue);
    //    }
    //}


    [SerializeField]
    private SliderControl sliderControl;


    private void OnEnable()
    {
        sliderControl.EvtSliderValueChanged += ThrustInputChanged;
        
    }


    // rocket only needs to know when thrust changes, can store the last input in a variable and change that through listening to this event
    public static event Action<float> EvtThrustInputChanged = delegate { };




    private void Start()
    {
        ThrustInputChanged(0f);


    //    var gamePlayActionMap = playerControls.FindActionMap("XRI RightHand Interaction");

    //    pressA = gamePlayActionMap.FindAction("Press A");
     //   pressB = gamePlayActionMap.FindAction("Press B");


        //   pressA.performed += SetThrustToOne;
    }

    //private void SetThrustToOne(InputAction.CallbackContext obj)
    //{
    //    // ThrustInputChanged(0.9f);
    //}







    //cap the thrust here so it doesn't send out weird stuff for the particles.


    // the goal is to have sliders, so this should simply send out a value between 0 and 1 every time it changes. 
    // the rocket then decides how to translate that into actual force
    // for now could use button to increase and decrease (and add a check to not do anything if decreasing and already at 0


    //private void Update()
    //{
    //    if (pressA.IsPressed())
    //    {
    //        ThrustSliderValue += Time.deltaTime;

    //        // ThrustInputChanged(ThrustSliderValue);
    //    }
    //    else if(pressB.IsPressed())
    //    {
    //        ThrustSliderValue -= Time.deltaTime;
    //        //    ThrustInputChanged(ThrustSliderValue);
    //    }

    //}



    //not really necessary to rout through this script


    public void ThrustInputChanged(float newInput)
    {
        //at 0 input this method still needs to run once to inform listeners (thrust/particles etc.)
        //if (newInput <= 0.01f)
        //    noThrustInput = true;

        //   currentForce = newInput * engine.forward;


        //  currentFanRotationSpeed = newInput * maxFanRotationSpeed + (1f - newInput) * minFanRotationSpeed;



        EvtThrustInputChanged(newInput);
        //   EvtThrustChanged(currentForce);
    }




    private void OnDisable()
    {
        sliderControl.EvtSliderValueChanged += ThrustInputChanged;
    }
}

