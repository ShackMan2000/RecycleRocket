using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{

    Transform SnapPoint { get; }


  //  bool IsGrabbed { get; }


    Transform StartGrabbing(Transform handAnchor);

    void OnEnterRange();

    void OnExitRange();

    void SetClosestOne();

    void StopGrabbing();


  //  public void MoveToHand(Transform t);


}
