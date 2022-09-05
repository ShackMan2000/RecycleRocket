using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SliderControl : MonoBehaviour, IGrabbable
{


    [SerializeField]
    private TextMeshProUGUI currentValueText;

    public Transform SnapPoint { get => handle; }

    [SerializeField]
    private Transform handle, handleSlave;


    public Vector3 localPositionTemp;

    [SerializeField]
    private bool goesIntoNegative;

    public float maxSlide = 1f;

    Material defaultMat;

    [SerializeField] Material grabbedMat;

    MeshRenderer handleRenderer;

    public Transform testHAnd;

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

    float offsetOnGrab;





    [SerializeField]
    private float startValue;

    [SerializeField]
    private Vector2 sliderRange;


    private Transform target;



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

    public Transform sliderBase;

    private Vector3 handleLocalStartPos;

    public void StartGrabbing(Transform t)
    {
        handleLocalStartPos = handle.transform.localPosition;

        target = t;
        isGrabbed = true;
        handleRenderer.material = grabbedMat;

        Vector3 targetPosition = target.transform.position;

        

        handInLocalSpace = Vector3.Scale(sliderBase.InverseTransformPoint(target.position), sliderBase.lossyScale);
        offsetOnGrab = handInLocalSpace.z;// - handle.localPosition.z;


      //  handle.localPosition = new Vector3(handle.localPosition.x, handle.localPosition.y, handInLocalSpace.z);// + offsetOnGrab);

        //  localPositionTemp =  handle.localPosition;
        // // handle.position = targetPosition;
        // // float clampedZ = Mathf.Clamp(handle.localPosition.z - offsetOnGrab, -maxSlide, maxSlide);
        //  handle.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, handInLocalSpace.z - offsetOnGrab);


        //  handInLocalSpace = Vector3.Scale(handleSlave.InverseTransformPoint(t.position), handleSlave.lossyScale);
        ////  offsetOnGrab = handInLocalSpace.z;// - handle.localPosition.z;
        //  localPositionTemp = handleSlave.localPosition;
        //  handleSlave.position = targetPosition;
        //  // float clampedZ = Mathf.Clamp(handle.localPosition.z - offsetOnGrab, -maxSlide, maxSlide);
        //  handleSlave.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, handleSlave.localPosition.z);

    }


    public Vector3 handInLocalSpace;

    public float handMovedZ;
    


    private void Update()
    {
        if (isGrabbed)
        {
            MoveToHand();


            //handInLocalSpace = Vector3.Scale(handle.InverseTransformPoint(target.position), handle.lossyScale);
            //offsetOnGrab = handInLocalSpace.z - handle.transform.localPosition.z;

        }

        //if (target != null)
        //{
        //    handInLocalSpace = Vector3.Scale(handle.InverseTransformPoint(target.position), handle.lossyScale);
        //    offsetOnGrab = handInLocalSpace.z;// - handle.localPosition.z;
        //}
    }


    public float handMovedSinceGrab;

    public void MoveToHand()
    {
        float newHandPosition = Vector3.Scale(sliderBase.InverseTransformPoint(target.position), sliderBase.lossyScale).z;

        handMovedSinceGrab = newHandPosition - offsetOnGrab;
        float clampedZ = Mathf.Clamp(handleLocalStartPos.z + handMovedSinceGrab, -maxSlide, maxSlide);

        handle.localPosition = new Vector3(handleLocalStartPos.x, handleLocalStartPos.y, clampedZ);


        //  localPositionTemp = handle.localPosition;

        //  Vector3 targetPosition = target.transform.position;
        //  handle.position = targetPosition;

        //  handInLocalSpace = Vector3.Scale(handle.InverseTransformPoint(target.position), handle.lossyScale);

        //  handle.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, handInLocalSpace.z - offsetOnGrab);

        //  handle.localPosition = new Vector3(localPositionTemp.x, localPositionTemp.y, clampedZ);

        //EvtSliderValueChanged(SliderValue);
        //OnSliderValueChanged.Invoke(SliderValue);
    }



    public void MoveWithHacker(float value)
    {
  

    }



    public void ForceStopGrabbing()
    {
        //inform the hand
        //EvtForcedStopGrabbing();
        StopGrabbing();
    }



    public void StopGrabbing()
    {
        handleRenderer.material = defaultMat;
        isGrabbed = false;
    }
}
