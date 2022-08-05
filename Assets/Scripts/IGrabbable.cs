using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{

    Transform snapPoint { get; }




    public void MoveToHand(Transform t);


}
