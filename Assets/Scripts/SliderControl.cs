using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour, IGrabbable
{
    public Transform SnapPoint { get => transform; }


    //   public Transform TESTtarget;

    public Vector3 localPositionTemp;



    public float maxSlide = 1f;

    public float SliderValue
    {
        get
        {
            return (transform.localPosition.z + maxSlide) / (maxSlide * 2f);
        }
    }

    private bool isGrabbed;



    private Transform target;

    //private void Update()
    //{
    //    MoveToHand(TESTtarget);
    //}

    public event Action<float> EvtSliderValueChanged = delegate { };



    public void StartGrabbing(Transform t)
    {
        target = t;
        isGrabbed = true;
    }






    private void Update()
    {
        if (isGrabbed)
            MoveToHand();
    }





    public void MoveToHand()
    {
        localPositionTemp = transform.localPosition;


        transform.position = target.transform.position;


        float clampedZ = Mathf.Clamp(transform.localPosition.z, -maxSlide, maxSlide);

        transform.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, clampedZ);

        EvtSliderValueChanged(SliderValue);
    }


    public void StopGrabbing()
    {
        isGrabbed = false;
    }
}
