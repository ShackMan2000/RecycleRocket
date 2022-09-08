using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SliderC : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI currentValueText;

    [SerializeField] GrabPoint grabPoint;

    private Transform grabPointTransform;

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
                currentValue = grabPointTransform.localPosition.z * (1f / maxSlide);
            else
                currentValue = (grabPointTransform.localPosition.z + maxSlide) / (maxSlide * 2f);

            currentValueText.text = currentValue.ToString("F1");
            return currentValue;
        }
    }

    bool isGrabbed;

    float offsetOnGrab;

    [SerializeField] Transform sliderBase;


    Vector3 handleLocalStartPos;

    [SerializeField] float startValue;

    [SerializeField] Vector2 sliderRange;


    Transform target;

    float handMovedSinceGrab;

    Vector3 handInLocalSpace;

    

  //  public event Action<float> EvtSliderValueChanged = delegate { };
    //public event Action EvtForcedStopGrabbing = delegate { };


    public UnityEvent<float> OnSliderValueChanged;

    private void Awake()
    {
        grabPointTransform = grabPoint.transform;

        float startPositionZ = goesIntoNegative? startValue *  maxSlide * 2f : (startValue * maxSlide * 2f) - maxSlide;

        grabPointTransform.localPosition = new Vector3(grabPointTransform.localPosition.x, grabPointTransform.localPosition.y, startPositionZ);

        OnSliderValueChanged.Invoke(SliderValue);

        DebugVR.TrackValue(this, nameof(offsetOnGrab));
        DebugVR.TrackValue(this, nameof(handInLocalSpace));
    }

    private void OnEnable()
    {
        grabPoint.OnStartGrabbing += StartGrabbing;
        grabPoint.OnStopGrabbing += StopGrabbing;
    }



    public void StartGrabbing(Transform grabber)
    {
        target = grabber;
        isGrabbed = true;
        handleLocalStartPos = grabPointTransform.transform.localPosition;

        handInLocalSpace = Vector3.Scale(sliderBase.InverseTransformPoint(target.position), sliderBase.lossyScale);
        offsetOnGrab = handInLocalSpace.z;
    }




    private void Update()
    {
        if (isGrabbed)
        {
            MoveToHand();            
        }       
    }



    public void MoveToHand()
    {
        float newHandPosition = Vector3.Scale(sliderBase.InverseTransformPoint(target.position), sliderBase.lossyScale).z;
        handMovedSinceGrab = newHandPosition - offsetOnGrab;

        float clampedZ = Mathf.Clamp(handleLocalStartPos.z + handMovedSinceGrab, -maxSlide, maxSlide);
        grabPointTransform.localPosition = new Vector3(handleLocalStartPos.x, handleLocalStartPos.y, clampedZ);

        OnSliderValueChanged.Invoke(SliderValue);
    }



    public void StopGrabbing()
    {
        isGrabbed = false;
    }


    [ContextMenu("SetToOne")]
    public void SetToOne()
    {
        OnSliderValueChanged.Invoke(1f);
    }


    private void OnDisable()
    {
        grabPoint.OnStartGrabbing -= StartGrabbing;
        grabPoint.OnStopGrabbing -= StopGrabbing;
    }

}
