using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{


    public Transform hand;


    public Vector3 rot;

    private void Update()
    {
        transform.rotation = hand.rotation;

        rot = transform.localEulerAngles;

    }

}
