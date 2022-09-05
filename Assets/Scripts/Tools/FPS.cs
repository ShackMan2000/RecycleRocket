using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int refreshRate;
    int frameCounter;
    float totalTime;


    private void Update()
    {
        if (frameCounter == refreshRate)
        {
            float averageFPS = (1f / (totalTime / refreshRate));
            frameCounter = 0;
            totalTime = 0f;
        }
        else
        {
            totalTime += Time.deltaTime;
            frameCounter++;
        }
    }


}
