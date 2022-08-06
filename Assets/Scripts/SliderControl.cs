using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControl : MonoBehaviour, IGrabbable
{
    public Transform snapPoint { get => transform; }


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


    //private void Update()
    //{
    //    MoveToHand(TESTtarget);
    //}

    public event Action<float> EvtSliderValueChanged = delegate { };



    private void Awake()
    {



    }




    public void MoveToHand(Transform t)
    {
        localPositionTemp = transform.localPosition;


        //transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
        transform.position = t.transform.position;


        float clampedZ = Mathf.Clamp(transform.localPosition.z, -maxSlide, maxSlide);

        transform.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, clampedZ);

        EvtSliderValueChanged(SliderValue);
    }
}
