using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    [SerializeField]
    private Camera vrCam;



    void Update()
    {

        Vector3 targetDirection = transform.position - vrCam.transform.position;

        transform.rotation = Quaternion.LookRotation(targetDirection);
    }


}
