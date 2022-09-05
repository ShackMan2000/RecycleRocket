using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandGrabber : MonoBehaviour
{




    List<IGrabbable> grabbablesInRange;

    public List<Transform> debugGrabbableInRange;


    private IGrabbable grabbedObject;


    Transform grabbedSnappoint;

    [SerializeField]
    private InputActionAsset playerControls;

    private InputAction pressA, pressB, grab;

    Vector3 handOriginalLocaPosition;
    Vector3 handOriginalRotation;


    [SerializeField]
    private Transform handModel;


    public bool isPushingGrabButton;
    bool isGrabbing;
    public float grabValue;



    // rocket only needs to know when thrust changes, can store the last input in a variable and change that through listening to this event
    public static event Action<float> EvtTryingToGrabSomethingNew = delegate { };

    // check some kind of min distance


    private void Start()
    {
        grabbablesInRange = new List<IGrabbable>();

        var gamePlayActionMap = playerControls.FindActionMap("XRI RightHand Interaction");

        grab = gamePlayActionMap.FindAction("Grab");

        handOriginalLocaPosition = handModel.transform.localPosition;
        handOriginalRotation = handModel.transform.localEulerAngles;
    }




    private void OnTriggerEnter(Collider other)
    {
        IGrabbable thingToGrab = other.GetComponent<IGrabbable>();

        if (thingToGrab != null)
            NewGrabbableInRange(thingToGrab);
    }






    void NewGrabbableInRange(IGrabbable thingToGrab)
    {
        if (!grabbablesInRange.Contains(thingToGrab))
        {
            grabbablesInRange.Add(thingToGrab);
            thingToGrab.OnEnterRange();
            GetClosestGrabbableInRange().SetClosestOne();
        }
        else
            print("WARNING, grabbable already in list, shouldn't happen because TriggerExit should remove it");
    }



    private void OnTriggerExit(Collider other)
    {
        IGrabbable thingToGrab = other.GetComponent<IGrabbable>();

        if (thingToGrab != null)
        {
            if (grabbablesInRange.Contains(thingToGrab))
            {
                grabbablesInRange.Remove(thingToGrab);
                thingToGrab.OnExitRange();
            }
            else
            {
                print("WARNING, grabbable not in list, shouldn't happen because TriggerEnter should have added it");
            }
        }
    }


    private void Update()
    {
        ReadGrabInput();
    }


    private void ReadGrabInput()
    {
        grabValue = grab.ReadValue<float>();

        //start grabbing
        if (!isPushingGrabButton && grabValue > 0.05f)
        {
            isPushingGrabButton = true;
            TryToGrabSomething();
        }
        //stop grabbing
        else if (isPushingGrabButton && grabValue < 0.05f)
        {
            isPushingGrabButton = false;

            if (grabbedObject != null)
                StopGrabbing();
        }
    }




    private void LateUpdate()
    {
        if (grabbedSnappoint)
        {
            handModel.rotation = grabbedSnappoint.rotation;
            handModel.position = grabbedSnappoint.position;
        }
    }


    private void TryToGrabSomething()
    {
        if (grabbablesInRange.Count == 0) return;

        grabbedObject = GetClosestGrabbableInRange();
        grabbedSnappoint = grabbedObject.StartGrabbing(transform);


    }

    private void StopGrabbing()
    {
        grabbedObject.StopGrabbing();
        grabbedObject = null;
        grabbedSnappoint = null;

        handModel.transform.localPosition = handOriginalLocaPosition;
        handModel.transform.localEulerAngles = handOriginalRotation;
    }





    private IGrabbable GetClosestGrabbableInRange()
    {
        float closestDistance = 11110f;
        IGrabbable closestGrabbable = null;

        for (int i = 0; i < grabbablesInRange.Count; i++)
        {
            if (Vector3.Distance(transform.position, grabbablesInRange[i].SnapPoint.position) < closestDistance)
            {
                closestGrabbable = grabbablesInRange[i];
                closestDistance = Vector3.Distance(transform.position, grabbablesInRange[i].SnapPoint.position);
            }
        }

        return closestGrabbable;
    }





  

}
