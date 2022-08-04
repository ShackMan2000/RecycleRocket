using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketSpeedText : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI rocketSpeedText;

    [SerializeField]
    private Rigidbody rocketRigidBody;


    [SerializeField]
    private RocketPhysics rocketPhysics;



    private void Update()
    {
        rocketSpeedText.text = rocketRigidBody.velocity.ToString("F1") + " avg:" + rocketPhysics.GetAbsoluteFallSpeed().ToString("F1");
    }

}
