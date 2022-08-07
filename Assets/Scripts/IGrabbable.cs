using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{

    Transform SnapPoint { get; }


  //  bool IsGrabbed { get; }


    public void StartGrabbing(Transform handAnchor);

    public void StopGrabbing();


  //  public void MoveToHand(Transform t);


}
