using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SliderC : MonoBehaviour, IGrabbable
{


    [SerializeField]
    private TextMeshProUGUI currentValueText;


    [SerializeField] Transform snappointHandle;
    public Transform SnapPoint { get => snappointHandle; }

    [SerializeField]
    private Transform handle;

    public Vector3 localPositionTemp;

    [SerializeField]
    private bool goesIntoNegative;

    public float maxSlide = 1f;

    Material defaultMat;

    [SerializeField] Material inRangeMat, closestOneMat, grabbedMat;

    MeshRenderer handleRenderer;


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
        handleRenderer = handle.GetComponent<MeshRenderer>();
        defaultMat = handleRenderer.material;
        //into negative not done yet
        float startPositionZ = goesIntoNegative? startValue *  maxSlide * 2f : (startValue * maxSlide * 2f) - maxSlide;

        handle.localPosition = new Vector3(handle.localPosition.x, handle.localPosition.y, startPositionZ);

        OnSliderValueChanged.Invoke(SliderValue);

        DebugVR.TrackValue(this, nameof(offsetOnGrab));
        DebugVR.TrackValue(this, nameof(handInLocalSpace));

    }



    public Transform StartGrabbing(Transform t)
    {

        target = t;
        isGrabbed = true;
        handleRenderer.material = grabbedMat;
        handleLocalStartPos = handle.transform.localPosition;

        Vector3 targetPosition = target.transform.position;        

        handInLocalSpace = Vector3.Scale(sliderBase.InverseTransformPoint(target.position), sliderBase.lossyScale);
        offsetOnGrab = handInLocalSpace.z;

        return SnapPoint;
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
        handle.localPosition = new Vector3(handleLocalStartPos.x, handleLocalStartPos.y, clampedZ);

        OnSliderValueChanged.Invoke(SliderValue);
    }



    public void StopGrabbing()
    {
        handleRenderer.material = inRangeMat;
        isGrabbed = false;
    }


    public void OnEnterRange()
    {
        handleRenderer.material = inRangeMat;
    }

    public void SetClosestOne()
    {
        handleRenderer.material = closestOneMat;
    }

    public void OnExitRange()
    {
        handleRenderer.material = defaultMat;
    }
}
