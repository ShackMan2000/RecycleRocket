using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickLever : MonoBehaviour
{
    //or maybe just use numbers and  0 means no movement
    public float angle, signedX;

    public float minXrotation, maxXrotation, minYrotation, maxYrotation, minZrotation, maxZrotation;
    public float minXmovement, maxXmovement, minYmovement, maxYmovement, minZmovement, maxZmovement;

    private Vector3 localStartPosition;

    //would be really useful to move it and log it, e.g. move cube, hit logXPosition button etc.
    Rigidbody rb;

    public bool removeEnergy;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        localStartPosition = transform.localPosition;
    }

    //actually LateFixedUpdate thanks to Script Execution Order settings
    // no clue why I need this to be late update??
    private void FixedUpdate()
    {
        //just clamp all the values

      ClampLocalPosition();

        ClampLocalRotation();

        //x = transform.localRotation.x;

        //  transform.localRotation = Quaternion.Euler

    }

    private void ClampLocalPosition()
    {
        float newX = Mathf.Clamp(transform.localPosition.x, minXmovement, maxXmovement);
        float newY = Mathf.Clamp(transform.localPosition.y, minYmovement, maxYmovement);
        float newZ = Mathf.Clamp(transform.localPosition.z, minZmovement, maxZmovement);


        transform.localPosition = localStartPosition;
       // transform.localPosition = new Vector3(newX, newY, newZ);
    }


    public Transform otherCube;
    private void ClampLocalRotation()
    {

        //if currentRot is > 0 && 


        float newX = Mathf.Clamp(transform.localRotation.eulerAngles.x, minXrotation, maxXrotation);
        float newY = Mathf.Clamp(transform.localRotation.eulerAngles.y, minYrotation, maxYrotation);
        float newZ = Mathf.Clamp(transform.localRotation.eulerAngles.z, minZrotation, maxZrotation);

     

        signedX = (transform.localEulerAngles.x + 180f) % 360f - 180f;
        angle = Quaternion.Angle(transform.localRotation, otherCube.localRotation);


        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, newY, newZ);
      //  transform.localRotation = new Quaternion(transform.localRotation.x, newY, 0f, transform.localRotation.w);
      if(removeEnergy)
          rb.angularVelocity = new Vector3(0f, 0f, 0f);

    }



}
