using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSliderMultiAxis : MonoBehaviour//, IGrabbable
{


    public Transform SnapPoint { get => handle; }

    [SerializeField]
    private Transform handle;

    //   public Transform TESTtarget;

    [SerializeField]
    private bool isLockedToAxis;


    [SerializeField]
    private float freeAxisRange;


    private Vector3 handleOriginalLocalPosition;


    public Vector3 localPositionTemp;



    [SerializeField]
    private Vector2 lockedAxis = Vector2.zero;

    public float maxSlide = 1f;

    //public float SliderValue
    //{
    //    get
    //    {
    //        return (transform.localPosition.z + maxSlide) / (maxSlide * 2f);
    //    }
    //}

    private bool isGrabbed;



    [SerializeField]
    private Transform inbetweenTarget, handAnchor;



    public event Action<Vector2> EvtSliderValueChanged = delegate { };




    private void Awake()
    {
        handleOriginalLocalPosition = handle.transform.localPosition;
    }

    public void StartGrabbing(Transform t)
    {
        handAnchor = t;
        handAnchor.transform.position = handle.transform.position;
        isGrabbed = true;
    }





    private void Update()
    {
        if (isGrabbed)
            MoveToHand();
    }



    public float distanceFromOrigin;




    //sucks right now, needs to find new axis when the handle is in the middle, not the anchor
    public void MoveToHand()
    {

        inbetweenTarget.position = handAnchor.position;

        // move han


        if (handle.localPosition.magnitude < freeAxisRange)
            isLockedToAxis = false;


        distanceFromOrigin = Vector2.Distance(new Vector2(inbetweenTarget.localPosition.x, inbetweenTarget.localPosition.z), new Vector2(handle.localPosition.x, handle.localPosition.z));


        if (!isLockedToAxis)
        {
            if (distanceFromOrigin > freeAxisRange)
            {
                isLockedToAxis = true;
                handAnchor.transform.position = handle.transform.position;


                // check in which axis it was moved more
                if (Mathf.Abs(inbetweenTarget.localPosition.x) > Mathf.Abs(inbetweenTarget.localPosition.z))
                    lockedAxis = new Vector2(1f, 0f);
                else
                    lockedAxis = new Vector2(0f, 1f);
            }
        }
        else if (distanceFromOrigin < freeAxisRange)
            isLockedToAxis = false;


        //only move the actual handle if we know which axis, otherwise it's gonna look wobbly in the center
        if (isLockedToAxis)
        {


            float handleMovedX = Mathf.Clamp(inbetweenTarget.transform.localPosition.x, -maxSlide, maxSlide) * lockedAxis.x;
            float handleMovedZ = Mathf.Clamp(inbetweenTarget.transform.localPosition.z, -maxSlide, maxSlide) * lockedAxis.y;


            handle.transform.localPosition = new Vector3(handleMovedX, handleOriginalLocalPosition.y, handleMovedZ);

            EvtSliderValueChanged(new Vector2(handleMovedX * (1f / maxSlide), handleMovedZ * (1f / maxSlide)));
        }


    }



    public void StopGrabbing()
    {
        isGrabbed = false;
        handle.transform.localPosition = handleOriginalLocalPosition;
        isLockedToAxis = false;

        EvtSliderValueChanged(new Vector2(0f, 0f));
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
