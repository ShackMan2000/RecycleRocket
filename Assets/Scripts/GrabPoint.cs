using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPoint : MonoBehaviour, IGrabbable
{
    public Transform SnapPoint => transform;  

    Material defaultMat;

    [SerializeField] Material inRangeMat, closestOneMat, grabbedMat;

    MeshRenderer handleRenderer;

    public event Action OnEnteredRange = delegate { };
    public event Action OnSetClosestOne = delegate { };
    public event Action OnExitRange = delegate { };
    public event Action<Transform> OnStartGrabbing = delegate { };
    public event Action OnStopGrabbing = delegate { };






    private void Awake()
    {
        handleRenderer = GetComponent<MeshRenderer>();
        defaultMat = handleRenderer.material;
    }


        public void EnterRange()
    {
        OnEnteredRange();
        handleRenderer.material = inRangeMat;

    }

    public void ExitRange()
    {
        handleRenderer.material = defaultMat;
        OnExitRange();
    }

    public void SetClosestOne()
    {
        handleRenderer.material = closestOneMat;
       // OnSetClosestOne();
    }

    public Transform StartGrabbing(Transform handAnchor)
    {
        handleRenderer.material = grabbedMat;

        OnStartGrabbing(handAnchor);
        return transform;
    }

    public void StopGrabbing()
    {
        handleRenderer.material = inRangeMat;
        OnStopGrabbing();
    }
}
