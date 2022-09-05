using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;




public class TrackedClass<T> : Itrackable where T : class
{
    private FieldInfo fieldInfo;
    private T trackedClass;

    public bool TrySetField(T trackedClass, string trackedValue)
    {
        this.trackedClass = trackedClass;

        fieldInfo = typeof(T).GetField(trackedValue, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        return (fieldInfo != null);
    }




    public string GetInfo()
    {
        return fieldInfo.Name + " " + fieldInfo.GetValue(trackedClass);
    }
}



public interface Itrackable
{
    string GetInfo();
}