using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class RocketData : ScriptableObject
{


    [field : SerializeField] public float RealHeight { get; private set; }
    [field: SerializeField] public float AddedHeight { get; private set; }
    public float TotalSpeed => RealSpeed + AddedSpeed;


    [field: SerializeField] public float RealSpeed { get; private set; }
    [field: SerializeField] public float AddedSpeed { get; private set; }
    public float TotalHeight => RealHeight + AddedHeight;


    public event Action RealHeightChanged = delegate { };
    public event Action FakeHeightChanged = delegate { };

    public event Action RealSpeedChanged = delegate { };
    public event Action FakeSpeedChanged = delegate { };



    public void ChangeRealHeight(float changeBy)
    {
        RealHeight += changeBy;
        RealHeightChanged();
    }

    public void ChangeAddedHeight(float changeBy)
    {
        AddedHeight += changeBy;
        FakeHeightChanged();
    }

    public void SetRealSpeed(float newValue)
    {
        RealSpeed = newValue;
        RealSpeedChanged();
    }

    public void SetFakeSpeed(float newValue)
    {
        AddedSpeed = newValue;
        FakeSpeedChanged();
    }


    //public void SetRealHeight(float newValue)
    //{
    //    RealHeight = newValue;
    //}

    //public void SetFakeHeight(float newValue)
    //{
    //    FakeHeight = newValue;
    //}

    public void Reset() => RealHeight = AddedHeight = RealSpeed = AddedSpeed = 0f;


}
