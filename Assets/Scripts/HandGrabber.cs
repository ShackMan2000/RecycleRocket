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



    private List<IGrabbable> grabbableInRange;

    public List<Transform> debugGrabbableInRange;


    [SerializeField]
    private InputActionAsset playerControls;

    private InputAction pressA, pressB, grab;

    private Vector3 handStartPosition;

    private float thrustSliderValue;

    //public float ThrustSliderValue
    //{
    //    get => thrustSliderValue;

    //    set
    //    {
    //        thrustSliderValue = Mathf.Clamp(value, 0f, 1f);
    //        ThrustInputChanged(thrustSliderValue);
    //    }
    //}



 


    //only use one event here? probably used input to get the percent of max thrust for particles
    //  public event Action<Vector3> EvtThrustChanged = delegate { };


    // rocket only needs to know when thrust changes, can store the last input in a variable and change that through listening to this event
    public static event Action<float> EvtTryingToGrabSomethingNew = delegate { };

    // check some kind of min distance


    private void Awake()
    {
        //   ThrustInputChanged(0f);

        handStartPosition = handModel.transform.localPosition;
        
        grabbableInRange = new List<IGrabbable>();



        var gamePlayActionMap = playerControls.FindActionMap("XRI RightHand Interaction");

        //pressA = gamePlayActionMap.FindAction("Press A");
        //pressB = gamePlayActionMap.FindAction("Press B");

        grab = gamePlayActionMap.FindAction("Grab");

        //   pressA.performed += SetThrustToOne;

        //grab.performed += CheckGrab;
        //grab.Enable();
    }

    private void CheckGrab(InputAction.CallbackContext obj)
    {

        print("grab");
    }


    private void OnTriggerEnter(Collider other)
    {
        IGrabbable thingToGrab = other.GetComponent<IGrabbable>();

        if (thingToGrab != null)
        {
            if (!grabbableInRange.Contains(thingToGrab))
            {
                grabbableInRange.Add(thingToGrab);
                debugGrabbableInRange.Add(other.transform);
            }
            else
            {
                print("WARNING, grabbable already in list, shouldn't happen because TriggerExit should remove it");
            }
        }
        //add grabbable to list


    }


    private void OnTriggerExit(Collider other)
    {
        IGrabbable thingToGrab = other.GetComponent<IGrabbable>();

        if (thingToGrab != null)
        {
            if (grabbableInRange.Contains(thingToGrab))
            {
                grabbableInRange.Remove(thingToGrab);
                debugGrabbableInRange.Remove(other.transform);
            }
            else
            {
                print("WARNING, grabbable not in list, shouldn't happen because TriggerEnter should have added it");
            }
        }
    }




    public bool isPushingGrabButton;
    public float grabValue;

    private void Update()
    {
        //if (pressA.IsPressed())
        //{
        //    ThrustSliderValue += Time.deltaTime;

        //    // ThrustInputChanged(ThrustSliderValue);
        //}
        //else if (pressB.IsPressed())
        //{
        //    ThrustSliderValue -= Time.deltaTime;
        //    //    ThrustInputChanged(ThrustSliderValue);
        //}

        grabValue = grab.ReadValue<float>();

        if (!isPushingGrabButton && grabValue > 0.05f)
        {
            isPushingGrabButton = true;
            TryToGrabSomething();
        }
        else if (isPushingGrabButton && grabValue < 0.05f)
        {
            isPushingGrabButton = false;
        }




        if(isGrabbingHandler)
            handModel.transform.position = grabbedTransform.position;
    }


    [SerializeField]
    private Transform handModel;


    private bool isGrabbingHandler;

    private Transform grabbedTransform;

    private void TryToGrabSomething()
    {

        print("trying to grab");
        if (grabbableInRange.Count == 0) return;

        isGrabbingHandler = true;
        grabbedTransform = grabbableInRange[0].trans;
        //place the hand model at the cube

       


        //then check distance of the grabbable and the anchor, if it is too big, 

        //grab the closest thing that can be grabbed.


    }


    // hand should have a list of Igrabbable 





    public void ThrustInputChanged(float newInput)
    {
        //at 0 input this method still needs to run once to inform listeners (thrust/particles etc.)
        //if (newInput <= 0.01f)
        //    noThrustInput = true;

        //   currentForce = newInput * engine.forward;


        //  currentFanRotationSpeed = newInput * maxFanRotationSpeed + (1f - newInput) * minFanRotationSpeed;



        EvtTryingToGrabSomethingNew(newInput);
        //   EvtThrustChanged(currentForce);
    }

}
