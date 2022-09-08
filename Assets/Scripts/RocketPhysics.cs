using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPhysics : MonoBehaviour
{


    private Rigidbody rb;

    [SerializeField]
    private float maxFallSpeed = -10f;

    public float speedIncrease, landingBreaksPercent;

    public bool enginesStarted;

    private float xRotationForce, yRotationForce;

    public Vector3 velocity;


    [SerializeField]
    private float maxSpeedForLanding;

    [SerializeField]
    private float maxVelocity;


    [SerializeField]
    private float minFallSpeedDuringLanding;

    public Queue<Vector3> averageSpeed;

    [SerializeField] Height height;


    [SerializeField]
    private GameSettings settings;


    float fakeSpeed;

    public static event Action<Vector3> EvtRocketExploded = delegate { };


    [SerializeField]
    private float launchForceMulti, rotationForceMulti, landingBurnForceMulti;



    public bool hasBeenLaunched;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        averageSpeed = new Queue<Vector3>();
    }

    private void Start()
    {
        DebugVR.TrackValue(this, nameof(velocity));
        //   DebugVR.TrackValue(height, ("realHeight"));

    }

    //private void Update()
    //{
    //    if (rb.velocity.y < maxFallSpeed)
    //        rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, rb.velocity.z);
    //}





    public void ChangeLaunchForce(float newForce)
    {

        //just for now to test without rocket tipping over
        if (!enginesStarted && newForce > 0)
        {
            Launch();
        }


        speedIncrease = newForce * launchForceMulti;
    }


    void Launch()
    {
        height.SetRealHeight(transform.position.y);
        height.SetFakeHeight(0f);

        fakeSpeed = 0f;
        rb.isKinematic = false;
        enginesStarted = true;
    }


    public void ChangeLandingBreaksPercent(float newPercent)
    {
        landingBreaksPercent = newPercent;// * landingBurnForceMulti;
    }

    public void ChangeXrotationForce(float sliderValue)
    {
        xRotationForce = sliderValue * rotationForceMulti;
    }

    public void ChangeYRotationForce(float sliderValue)
    {
        yRotationForce = sliderValue * rotationForceMulti;
    }




    float speed;


    private void FixedUpdate()
    {
        if (!enginesStarted) return;




        speed += speedIncrease * Time.fixedDeltaTime;

        //this changes the velocity by exactly that value
        rb.AddForce(transform.up * speedIncrease);

        velocity = rb.velocity;

        // fucking hell, need to save fakespeed;
        // calculate some drag here
        // need to account for falling as well, maybe make custom extension of float the returns the removed amount

        if (rb.velocity.y > maxVelocity)
        {
            height.AddRealHeight(maxVelocity * Time.fixedDeltaTime);
            height.AddFakeHeight((rb.velocity.y - maxVelocity) * Time.fixedDeltaTime);

        }
        else
        {
            height.AddRealHeight(rb.velocity.y * Time.fixedDeltaTime);
        }



        velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));



        rb.velocity = velocity;


        UpdateAverageSpeed(rb.velocity);



        AddRotationForce();

        AddLandingBreakForce();

    }





    private void AddLandingBreakForce()
    {
        if (rb.velocity.y > 0f) return;

        float forceToAdd = landingBreaksPercent * (-rb.velocity.y + 9.7f);
        forceToAdd -= minFallSpeedDuringLanding;

        rb.AddForce(forceToAdd * transform.up);
    }

    private void AddRotationForce()
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
















}
