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

    [SerializeField] private Renderer skinRenderer;

    private Rigidbody rb;

    [SerializeField]
    private float showControllerHandDistance = 0.05f;


    public int activeCollisions;

    [SerializeField]
    private Material noColMaterial, collisionMaterial;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }


    private void Update()
    {
        float distanceToControllerHand = Vector3.Distance(transform.position, controllerHand.position);

        controllerHandRenderer.enabled = distanceToControllerHand > showControllerHandDistance;

        if (activeCollisions == 0)
        {
            transform.rotation = controllerHand.rotation;
            transform.position = controllerHand.position;
        }

    }




    private void OnCollisionEnter(Collision collision)
    {
        activeCollisions++;

        if (activeCollisions == 1)
            skinRenderer.material = collisionMaterial;
    }

    private void OnCollisionExit(Collision collision)
    {
        activeCollisions--;
        if (activeCollisions == 0)
            skinRenderer.material = noColMaterial;
    }



    private void FixedUpdate()
    {


        if (activeCollisions > 1)
        {
            SetPositionViaRigidBody();
            SetRotationViaRigidBody();
        }

    }

    private void SetPositionViaRigidBody()
    {
        // can't use rb.Move bc that only works with kinematic 
        Vector3 distanceToAnchorHand = (controllerHand.position - transform.position);
        rb.velocity = distanceToAnchorHand / Time.fixedDeltaTime;
    }

    private void SetRotationViaRigidBody()
    {
        Quaternion rotationDifference = controllerHand.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
