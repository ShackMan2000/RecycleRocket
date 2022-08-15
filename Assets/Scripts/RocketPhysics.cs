using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPhysics : MonoBehaviour
{


    private Rigidbody rb;

    [SerializeField]
    private float maxFallSpeed = -10f;

    public float currentLaunchForce, currentLandingBurnFoce;


    private float xRotationForce, yRotationForce;

    public Vector3 velocity;


    [SerializeField]
    private float maxSpeedForLanding;

    [SerializeField]
    private float maxVelocity;


    public Queue<Vector3> averageSpeed;


    [SerializeField]
    private GameSettings settings;


    public static event Action<Vector3> EvtRocketExploded = delegate { };


    [SerializeField]
    private SliderControl launchThrustSlider, landingBurnThrustSlider, rotateXslider, rotateYslider;


    [SerializeField]
    private float launchForceMulti, rotationForceMulti, landingBurnForceMulti;




    public bool hasBeenLaunched;

    private void OnEnable()
    {
        launchThrustSlider.EvtSliderValueChanged += ChangeLaunchForce;
        landingBurnThrustSlider.EvtSliderValueChanged += ChangeLandingBurnForce;
        rotateXslider.EvtSliderValueChanged += ChangeXrotationForce;
        rotateYslider.EvtSliderValueChanged += ChangeYRotationForce;

        rb = GetComponent<Rigidbody>();
        averageSpeed = new Queue<Vector3>();
    }


    private void Update()
    {
        if (rb.velocity.y < maxFallSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, rb.velocity.z);

    }



    public void ChangeLaunchForce(float newForce)
    {
        currentLaunchForce = newForce * launchForceMulti;
    }


    public void ChangeLandingBurnForce(float newForce)
    {
        currentLandingBurnFoce = newForce * landingBurnForceMulti;
    }

    private void ChangeXrotationForce(float sliderValue)
    {
        xRotationForce = sliderValue * rotationForceMulti;
    }

    private void ChangeYRotationForce(float sliderValue)
    {
        yRotationForce = sliderValue * rotationForceMulti;
    }




    private void FixedUpdate()
    {
        rb.AddForce(transform.up * (currentLaunchForce + currentLandingBurnFoce));

        velocity = rb.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));



        rb.velocity = velocity;
        UpdateAverageSpeed(rb.velocity);



        AddRotationForce();
    }




    public void AddRotationForce()
    {
        if (yRotationForce == 0f && xRotationForce == 0f)
            return;

        rb.AddRelativeTorque(new Vector3(yRotationForce, 0f, xRotationForce));
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenLaunched) return;


        // don't use rb.velocity because it's 0 after the collision, get the average of the last 0.x seconds
        if (GetVerticalSpeed() > maxSpeedForLanding)
        {
            //print(collision.relativeVelocity.z + " smaller than " + settings.maxSpeedForLanding);

            rb.isKinematic = true;
        }
        else
        {
            EvtRocketExploded(collision.contacts[0].point);
        }

    }

    private void UpdateAverageSpeed(Vector3 speed)
    {
        averageSpeed.Enqueue(speed);


        if (averageSpeed.Count > 20)
            averageSpeed.Dequeue();

    }



    public float GetVerticalSpeed()
    {
        float speed = 0f;

        foreach (var entry in averageSpeed)
        {
            speed += entry.y;
        }

        return speed / averageSpeed.Count;
    }




    private void OnDisable()
    {

    }













}
