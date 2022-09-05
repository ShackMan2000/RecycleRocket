using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTEst : MonoBehaviour
{

    public SliderControl sc;



    [ContextMenu("grab")]
    void StartGRabbing()
    {
        sc.StartGrabbing(transform);
    }



}
