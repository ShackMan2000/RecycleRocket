using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketChairTest : MonoBehaviour
{

    public Transform rocket;

    public Vector3 offset;


    private void Awake()
    {
        offset = transform.position - rocket.position;
    }


    private void Update()
    {
        transform.position = rocket.position + offset;
    }


}
