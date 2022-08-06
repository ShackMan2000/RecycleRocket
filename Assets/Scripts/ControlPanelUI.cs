using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlPanelUI : MonoBehaviour
{

    // read out values from all kinds of stuff and display it here
    // use for debugging as well

    //maybe should use debugging separately


    [SerializeField]
    private TextMeshProUGUI rocketSpeedText, thrustSliderValueText;

    [SerializeField]
    private Rigidbody rocketRigidBody;


    [SerializeField]
    private SliderControl thrustSlider;

    [SerializeField]
    private RocketPhysics rocketPhysics;



    private void Update()
    {
        rocketSpeedText.text = rocketRigidBody.velocity.ToString("F1") + " avg:" + rocketPhysics.GetAbsoluteFallSpeed().ToString("F1");

        thrustSliderValueText.text = thrustSlider.SliderValue.ToString("F1");
    }

}
