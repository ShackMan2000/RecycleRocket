using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandGrabber : MonoBehaviour
{

    // when the grab trigger button gets pressed, aka value bigger 0f, check if already grabbing
    // if not, enable grabbing


    // look for all interactables and grab the closest one. Could use an event, like when grab starts, a static event is sent out
    // and they all pass their position to the hand, which then checks what is closest
    // use a sphere collider you idiot


    // need to figure out how to use this for different hands...



    [SerializeField]
    private Transform anchorHelper;


    private List<IGrabbable> grabbableInRange;

    public List<Transform> debugGrabbableInRange;


    private IGrabbable grabbedObject;

    [SerializeField]
    private InputActionAsset playerControls;

    private InputAction pressA, pressB, grab;

    private Vector3 handOriginalLocaPosition; 

    public float maxDistanceHandToAnchor;

    public float currentDistanceHandToAnchor;


    [SerializeField]
    private Transform handModel, handAnchor;


  

    // rocket only needs to know when thrust changes, can store the last input in a variable and change that through listening to this event
    public static event Action<float> EvtTryingToGrabSomethingNew = delegate { };

    // check some kind of min distance


    //private void Awake()
    //{
    //    //   ThrustInputChanged(0f);

    //    handOriginalLocaPosition = handModel.transform.localPosition;

    //    grabbableInRange = new List<IGrabbable>();



    //    var gamePlayActionMap = playerControls.FindActionMap("XRI RightHand Interaction");

    //    //pressA = gamePlayActionMap.FindAction("Press A");
    //    //pressB = gamePlayActionMap.FindAction("Press B");

    //    grab = gamePlayActionMap.FindAction("Grab");

    //    //   pressA.performed += SetThrustToOne;

    //    //grab.performed += CheckGrab;
    //    //grab.Enable();
    //}




    //private void OnTriggerEnter(Collider other)
    //{
    //    IGrabbable thingToGrab = other.GetComponent<IGrabbable>();

    //    if (thingToGrab != null)
    //    {
    //        if (!grabbableInRange.Contains(thingToGrab))
    //        {
    //            grabbableInRange.Add(thingToGrab);
    //            debugGrabbableInRange.Add(other.transform);
    //        }
    //        else
    //        {
    //            print("WARNING, grabbable already in list, shouldn't happen because TriggerExit should remove it");
    //        }
    //    }
    //}


    //private void OnTriggerExit(Collider other)
    //{
    //    IGrabbable thingToGrab = other.GetComponent<IGrabbable>();

    //    if (thingToGrab != null)
    //    {
    //        if (grabbableInRange.Contains(thingToGrab))
    //        {
    //            grabbableInRange.Remove(thingToGrab);
    //            debugGrabbableInRange.Remove(other.transform);
    //        }
    //        else
    //        {
    //            print("WARNING, grabbable not in list, shouldn't happen because TriggerEnter should have added it");
    //        }
    //    }
    //}

    //public bool isPushingGrabButton;
    //public float grabValue;





    //private void Update()
    //{
    //    grabValue = grab.ReadValue<float>();

    //    //start grabbing
    //    if (!isPushingGrabButton && grabValue > 0.05f)
    //    {
    //        isPushingGrabButton = true;
    //        TryToGrabSomething();
    //    }
    //    //stop grabbing
    //    else if (isPushingGrabButton && grabValue < 0.05f)
    //    {
    //        isPushingGrabButton = false;

    //        if (grabbedObject != null)
    //            StopGrabbing();

    //    }

    //}


    //private void LateUpdate()
    //{
    //    MoveHand();

    //}

    //private void MoveHand()
    //{
    //    if (grabbedObject != null)
    //    {
    //        handModel.transform.position = grabbedObject.SnapPoint.position;

    //        CheckDistanceHandToGrabbedObject();
    //    }
    //}

    //private void CheckDistanceHandToGrabbedObject()
    //{

    //    //problem here is that there is always a distance bc hand is not at 000
    //    // for now just make distance bigger....
    //    currentDistanceHandToAnchor = Vector3.Distance(handModel.transform.position, handAnchor.transform.position);
    //    if (currentDistanceHandToAnchor > maxDistanceHandToAnchor)
    //    {
    //        StopGrabbing();
    //    }
    //}

    //private void StopGrabbing()
    //{
    //    grabbedObject.StopGrabbing();
    //    grabbedObject = null;
    //    handModel.transform.localPosition = handOriginalLocaPosition;
    //}

    //private void TryToGrabSomething()
    //{
    //    if (grabbableInRange.Count == 0) return;


    //    //change to grab the closest one
    //    grabbedObject = GetClosestGrabbableInRange();

    //    //for multiSlider this is redundant, but normal slider still needs it
    //    anchorHelper.transform.position = grabbedObject.SnapPoint.position;
    //    grabbedObject.StartGrabbing(anchorHelper);
    //}




    //private IGrabbable GetClosestGrabbableInRange()
    //{
    //    float closestDistance = 11110f;
    //    IGrabbable closestGrabbable = null;


    //    for (int i = 0; i < grabbableInRange.Count; i++)
    //    {
    //        if (Vector3.Distance(transform.position, grabbableInRange[i].SnapPoint.position) < closestDistance)
    //        {
    //            closestGrabbable = grabbableInRange[i];
    //            closestDistance = Vector3.Distance(transform.position, grabbableInRange[i].SnapPoint.position);
    //        }

    //    }


    //    return closestGrabbable;

    //}


    //public void ThrustInputChanged(float newInput)
    //{
    //    EvtTryingToGrabSomethingNew(newInput);
    //}

}
