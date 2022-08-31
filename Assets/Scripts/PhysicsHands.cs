using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Move the hand as close as possible to the hand controllers, but collide with the real world. 
/// Disable collisions when grabbing something.
/// Show the real hands when they are not in the same position
/// </summary>


[RequireComponent(typeof(Rigidbody))]
public class PhysicsHands : MonoBehaviour
{

    //better to use the hands, not the controller, so you can fine tune them and automatically use the same here
    [SerializeField]
    private Transform controllerHand;
    
    [SerializeField]
    private Renderer controllerHandRenderer;
    
    private Rigidbody rb;

    [SerializeField]
    private float showControllerHandDistance = 0.05f;




    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        float distanceToControllerHand = Vector3.Distance(transform.position, controllerHand.position);

        controllerHandRenderer.enabled = distanceToControllerHand > showControllerHandDistance;
    }




    private void FixedUpdate()
    {
        // can't use rb.Move bc that only works with kinematic rigidbodies
        //whatever the difference is this step, move it exactly like that, thus eliminating the difference
        rb.velocity = (controllerHand.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotationDifference = controllerHand.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
