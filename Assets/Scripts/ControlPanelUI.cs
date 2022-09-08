using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ControlPanelUI : MonoBehaviour
{

    // read out values from all kinds of stuff and display it here
    // use for debugging as well

    //maybe should use debugging separately


    [SerializeField]
    private TextMeshProUGUI rocketSpeedText, rocketHeightText, thrustSliderValueText, rotateForceText;

    [SerializeField]
    private Rigidbody rocketRigidBody;


    [SerializeField]
    private SliderC thrustSlider;

    [SerializeField]
    private RocketPhysics rocketPhysics;


    [SerializeField]
    private ControlSliderMultiAxis rotationControl;

    [SerializeField] RocketData rocketData;


    private void OnEnable()
    {
        rocketData.RealHeightChanged += RefreshUI;
        rocketData.FakeHeightChanged += RefreshUI;
        rocketData.RealSpeedChanged += RefreshUI;
        rocketData.FakeSpeedChanged += RefreshUI;
    }



    private void RefreshUI()
    {
        float speedKmh = rocketData.TotalSpeed * 3.6f;
        float fakeSpeed = rocketData.AddedSpeed * 3.6f;
        rocketSpeedText.text = $"speed: {speedKmh.ToString("F0")} (fake {fakeSpeed.ToString("F0")}";
        rocketHeightText.text = $"height: {rocketData.TotalHeight.ToString("F0")} (fake {rocketData.AddedHeight.ToString("F0")}";
    }



    private void Update()
    {
       //  = rocketRigidBody.velocity.ToString("F1") + " avg:" + rocketPhysics.GetVerticalSpeed().ToString("F1");

        // thrustSliderValueText.text = thrustSlider.SliderValue.ToString("F1");
    }




    private void ShowRotationForceText(Vector2 force)
    {

        rotateForceText.text = force.ToString();
    }





    private void OnDisable()
    {
        rocketData.RealHeightChanged -= RefreshUI;
        rocketData.FakeHeightChanged -= RefreshUI;
        rocketData.RealSpeedChanged -= RefreshUI;
        rocketData.FakeSpeedChanged -= RefreshUI;
    }
}
