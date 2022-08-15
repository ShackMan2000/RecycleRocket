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
    }







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

