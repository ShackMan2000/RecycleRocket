using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{

    // show the rocket as a hologram, including the axis
    // rotation is simply copied from the actual rocket

    //listen to ringmanager updating ringlist




    private Transform nextRing;


    





    private void Update()
    {
        transform.LookAt(nextRing);
    }


}

