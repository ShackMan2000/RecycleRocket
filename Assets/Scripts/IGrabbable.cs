using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    //could actually get rid of update, and have the grabber run an update that calls a method here when it is grabbing it.

    Transform SnapPoint { get; }


  //  bool IsGrabbed { get; }


    Transform StartGrabbing(Transform handAnchor);

    void EnterRange();

    void ExitRange();

    void SetClosestOne();

    void StopGrabbing();


  //  public void MoveToHand(Transform t);


}
