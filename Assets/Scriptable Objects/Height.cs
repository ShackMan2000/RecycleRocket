using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Height : ScriptableObject
{



    [field : SerializeField] public float RealHeight { get; private set; }

    [field: SerializeField] public float FakeHeight { get; private set; }


    public void AddRealHeight(float changeBy)
    {
        RealHeight += changeBy;
    }

    public void AddFakeHeight(float changeBy)
    {
        FakeHeight += changeBy;
    }

    public void SetRealHeight(float newValue)
    {
        RealHeight = newValue;
    }

    public void SetFakeHeight(float newValue)
    {
        FakeHeight = newValue;
    }

}
