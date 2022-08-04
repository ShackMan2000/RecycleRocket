using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPhysics : MonoBehaviour
{

    // listen to input from rocket thrust controller (which sends a float for thrust, thrust is always applied based on the rockets Z)
    // test that rocket flies to the side when it tilts and thrust is added.
    // cap speed (use magnitude so it's capped in all directions


    private Rigidbody rb;

    [SerializeField]
    private float maxFallSpeed = -10f;

    public float currentForce;

    private Vector3 velocity;


    [SerializeField]
    private float maxVelocity;


    public Queue<Vector3> averageSpeed;



    [SerializeField]
    private GameSettings settings;


    public static event Action<Vector3> EvtRocketExploded = delegate { };




    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        averageSpeed = new Queue<Vector3>();

        RocketThrustController.EvtThrustInputChanged += ChangeForce;
    }



    private void Update()
    {
        if (rb.velocity.y < maxFallSpeed)
            rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, rb.velocity.z);


        if (Input.GetKey(KeyCode.LeftArrow))
            AddRotationForce(new Vector2(-1f, 0f));
        else if (Input.GetKey(KeyCode.RightArrow))
            AddRotationForce(new Vector2(1f, 0f));


    }



    private float fallSpeedBeforeImpact;



    public void ChangeForce(float newForce)
    {
        currentForce = newForce * addForceMulti;
    }


    public Vector3 TestVelocity;

    [SerializeField]
    private float addForceMulti;

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * currentForce);

        velocity = rb.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity),
                            Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));

        //speedText.text = Vector3.Magnitude(velocity).ToString("F1") + " / " + Vector3.Magnitude(new Vector3(maxVelocity, maxVelocity, maxVelocity)).ToString("F1");
        //not clamping

        rb.velocity = velocity;
        TestVelocity = velocity;
        UpdateAverageSpeed(rb.velocity);
    }




    public float rotationForceMulti;


    // this needs to be changed so it adds the force in fixed update but makes sure that an input isn't lost
    // like frame could happen within a fixedupdate. though that doesn't matter for now, player won't feel it when using a joystick
    // unity simply checks for input in fixed update.

    // just use joystick, while it is being moved, it changes the Vector 2 that is applied all the time.
    // as soon as the player let's go, the joystick will move back into 0,0



    public void AddRotationForce(Vector2 force)
    {
        rb.AddRelativeTorque(new Vector3(force.x, 0f, force.y) * rotationForceMulti);
    }




    private void OnCollisionEnter(Collision collision)
    {

        if (GetAbsoluteFallSpeed() < settings.maxSpeedForLanding)
        {
            print(collision.relativeVelocity.z + " smaller than " + settings.maxSpeedForLanding);

            rb.isKinematic = true;
        }
        else
        {
            EvtRocketExploded(collision.contacts[0].point);
            print("Should be exploding");
        }

    }

    private void UpdateAverageSpeed(Vector3 speed)
    {
        averageSpeed.Enqueue(speed);


        if (averageSpeed.Count > 20)
            averageSpeed.Dequeue();

    }



    public float GetAbsoluteFallSpeed()
    {
        float speed = 0f;

        foreach (var entry in averageSpeed)
        {
            speed += entry.y;
        }

        return Mathf.Abs(speed / averageSpeed.Count);

    }




    private void OnDisable()
    {
        RocketThrustController.EvtThrustInputChanged -= ChangeForce;
    }













}
