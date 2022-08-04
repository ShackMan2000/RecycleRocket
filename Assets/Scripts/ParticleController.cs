using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleController : MonoBehaviour
{

    // Listen to change in thrust
    // Adjust Particle values


  
    [SerializeField]
    private ParticleSystem sys;

    [SerializeField]
    private RocketThrustController rocketThrust;


    [SerializeField]
    private float minStartSpeed, maxStartSpeed;


    [SerializeField]
    private Vector3 minPosition, maxPosition;

    [SerializeField]
    private float minEmission, maxEmission;

    private ParticleSystem.EmissionModule sysEmit;


    private ParticleSystem.MainModule sysMain;

    public bool inDebugMode;


    private void OnEnable()
    {

        RocketThrustController.EvtThrustInputChanged += SetStartSpeed;


        // smoke needs to move up and down, fire doesn't
        if (minPosition != maxPosition)
            RocketThrustController.EvtThrustInputChanged += SetPosition;


        if (minEmission != maxEmission)
            RocketThrustController.EvtThrustInputChanged += SetEmission;


        sysEmit = sys.emission;

        sysMain = sys.main;
    }




    private void SetStartSpeed(float thrustInPercent)
    {  
        float speed = thrustInPercent * maxStartSpeed + (1f - thrustInPercent) * minStartSpeed;

        sysMain.startSpeed = speed;
    }




    private void SetPosition(float thrustInPercent)
    {
        Vector3 pos = thrustInPercent * maxPosition + (1f - thrustInPercent) * minPosition;

        sys.transform.localPosition = pos;
    }



    private void SetEmission(float thrustInPercent)
    {
        float emission = thrustInPercent * maxEmission + (1f - thrustInPercent) * minEmission;
       
        sysEmit.rateOverTime = emission;

        if (inDebugMode)
            print(sysEmit.rateOverTime);
    }



    private void OnDisable()
    {
        RocketThrustController.EvtThrustInputChanged -= SetStartSpeed;

        if (minPosition != maxPosition)
            RocketThrustController.EvtThrustInputChanged -= SetPosition;

        if (minEmission != maxEmission)
            RocketThrustController.EvtThrustInputChanged -= SetEmission;
    }



    [ContextMenu("SetMinPosition")]
    private void SetMinPosition()
    {
        minPosition = sys.transform.localPosition;
    }

    [ContextMenu("SetMaxPosition")]
    private void SetMaxPosition()
    {
        maxPosition = sys.transform.localPosition;
    }
}

