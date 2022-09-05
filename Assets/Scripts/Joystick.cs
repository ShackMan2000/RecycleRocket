using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour//, IGrabbable
{

    public Transform stick, stickTarget, handAnchor;




    [SerializeField]
    private Transform snapPoint;

    public Transform SnapPoint => snapPoint;


    public event Action<float> EvtSliderValueChanged = delegate { };






  


    public float stickTargetmaxRange = 1f;

    //public float SliderValue
    //{
    //    get
    //    {
    //        return (transform.localPosition.z + maxSlide) / (maxSlide * 2f);
    //    }
    //}

    private bool isGrabbed;
    private Vector3 stickTargetOriginalPosition;

    public void StartGrabbing(Transform t)
    {
        handAnchor = t;
        isGrabbed = true;
    }






    private void Update()
    {
        if (isGrabbed)
            PointToHand();   
    }




    public void PointToHand()
    {

      


        //stickTarget follows hand, but is limited in local Y and range

        //just a bit easier to not convert local and world positions
        stickTargetOriginalPosition = stickTarget.localPosition;


        stickTarget.position = handAnchor.position;

        float clampedX = Mathf.Clamp(stickTarget.localPosition.x, -stickTargetmaxRange, stickTargetmaxRange);
        float clampedZ = Mathf.Clamp(stickTarget.localPosition.z, -stickTargetmaxRange, stickTargetmaxRange);

        stickTarget.localPosition = new Vector3(clampedX, stickTargetOriginalPosition.y, clampedZ);



        //Vector3 newtarget = stickTarget.position;
        //newtarget.z = stick.position.z;

        stick.LookAt(stickTarget);


        //localPositionTemp = transform.localPosition;


        //transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);

     //   transform.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, clampedZ);

     //   EvtSliderValueChanged(SliderValue);
    }






    public void StopGrabbing()
    {
        isGrabbed = false;
    }

    public void OnEnterRange()
    {
        throw new NotImplementedException();
    }

    public void SetClosestOne()
    {
        throw new NotImplementedException();
    }
}
