using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{



    public static (float newValue, float changedBy) Cap(this float value, float min, float max)
    {
        float changedBy = 0f;

        if(value > max)
        {
            changedBy = max - value;
            value = max;
        }
        else if( value < min)
        {
            changedBy = min - value;
            value = min;
        }

        return (value, changedBy);
    }



}
