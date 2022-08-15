using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderControl : MonoBehaviour, IGrabbable
{


    [SerializeField]
    private TextMeshProUGUI currentValueText;

    public Transform SnapPoint { get => handle; }

    [SerializeField]
    private Transform handle;


    public Vector3 localPositionTemp;

    [SerializeField]
    private bool goesIntoNegative;


    public float maxSlide = 1f;

    public float SliderValue
    {


        get
        {
            float currentValue = 0f;

            if (goesIntoNegative)
                currentValue = handle.localPosition.z * (1f / maxSlide);
            else
                currentValue = (handle.localPosition.z + maxSlide) / (maxSlide * 2f);



            currentValueText.text = currentValue.ToString("F1");
            return currentValue;

        }
    }

    private bool isGrabbed;






    [SerializeField]
    private float handleStartPosition;

    [SerializeField]
    private Vector2 sliderRange;


    private Transform target;



    public event Action<float> EvtSliderValueChanged = delegate { };
    public event Action EvtForcedStopGrabbing = delegate { };




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
        localPositionTemp = handle.localPosition;

        handle.position = target.transform.position;


        float clampedZ = Mathf.Clamp(handle.localPosition.z, -maxSlide, maxSlide);

        handle.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, clampedZ);

        EvtSliderValueChanged(SliderValue);
    }



    public void MoveWithHacker(float value)
    {
        //float newZ = value * maxSlide * 2f;

        //float clampedZ = Mathf.Clamp(newZ, -maxSlide, maxSlide);

        //transform.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, clampedZ);
        print(value);
        EvtSliderValueChanged(value);
    }



    public void ForceStopGrabbing()
    {
        //inform the hand
        EvtForcedStopGrabbing();
        StopGrabbing();
    }



    public void StopGrabbing()
    {
        isGrabbed = false;
    }
}
