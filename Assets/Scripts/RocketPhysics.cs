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

    public float speed;


    [SerializeField]
    private float maxSpeedForLanding;

    [SerializeField]
    private float maxUpSpeed;


    [SerializeField]
    private float minFallSpeedDuringLanding;

    public Queue<Vector3> averageSpeed;

    [SerializeField] RocketData data;


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
        DebugVR.TrackValue(this, nameof(speed));
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
        data.Reset();

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


    //maybe don't use gravity on the way up and simply prevent the slider from going down while launching...



    private void FixedUpdate()
    {


        if (!enginesStarted) return;

        speed += speedIncrease * Time.fixedDeltaTime;

        //gravity
        // speed -= 10f * Time.fixedDeltaTime;
        //account for falling as well later

        var cappedSpeed = speed.Cap(maxFallSpeed, maxUpSpeed);

        data.ChangeRealHeight(cappedSpeed.newValue * Time.fixedDeltaTime);
        rb.velocity = new Vector3(0f,cappedSpeed.newValue,0f);

        data.ChangeAddedHeight(-cappedSpeed.changedBy * Time.deltaTime);


        

        data.SetRealSpeed(speed);


        //UpdateAverageSpeed(rb.velocity);

        AddRotationForce();

        //rewrite for directly speed
      //  AddLandingBreakForce();

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

    //private void UpdateAverageSpeed(Vector3 speed)
    //{
    //    averageSpeed.Enqueue(speed);


    //    if (averageSpeed.Count > 20)
    //        averageSpeed.Dequeue();

    //}



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
        data.Reset();
    }








}
