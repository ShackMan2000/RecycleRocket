using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ZoomingGround : MonoBehaviour
{


    [SerializeField] MeshRenderer meshRenderer;

    [SerializeField] List<Texture2D> groundTextures;

    [SerializeField] float baseHeightLevel = 100f;

    [SerializeField] List<float> heightLevels;

    [SerializeField] Transform groundContainer;

    //use this to make sure player is in the air a little bit
    [SerializeField] float effectStartHeight;

    [SerializeField] RocketData heightSO;


    public float height;

    Material material;

    [SerializeField] float zoomPerSec;
 

    public Transform rocket;

    int scaleID;
    int textureID;

    private void Awake()
    {
        material = meshRenderer.material;
        SetHeightLevels();
        scaleID = Shader.PropertyToID("_Scale");
        textureID = Shader.PropertyToID("_Texture");
    }


    [ContextMenu("SetHeightLevels")]
    void SetHeightLevels()
    {
        heightLevels = new List<float>();

        heightLevels.Add(baseHeightLevel);

        for (int i = 1; i < groundTextures.Count; i++)
        {
            heightLevels.Add(heightLevels[i - 1] * 2f);
        }
    }


    private void Update()
    {
        height = rocket.position.y;
        AdjustTextureToZoom();


        if (heightSO.TotalHeight > effectStartHeight)
            ScaleGroundContainer();

    }


    int currentHeightLevel;

    void AdjustTextureToZoom()
    {
        SetCurrentHeightLevel();
        SetZoom();
    }


    void ScaleGroundContainer()
    {
     //yes, only use added height and start right away when it is bigger than 0

        float heightAboveEffectStart = heightSO.TotalHeight / effectStartHeight;
        float newScale = 1f / heightAboveEffectStart;
        
        groundContainer.localScale = new Vector3(newScale, newScale, newScale);


    }

    private void SetZoom()
    {
        float heightThisStep = heightLevels[currentHeightLevel] / 2f;
        float percentageOfCurrentLevel = (height - heightThisStep) / heightThisStep;

        material.SetFloat(scaleID, 2f - percentageOfCurrentLevel);
    }

    void SetCurrentHeightLevel()
    {
        int oldHeightLevel = currentHeightLevel;


        if (height > heightLevels.Last())
        {
           // Debug.Log("No more textures for the current height, capping height");
            //so it won't get scaled by next method, change that to scale the transform of the plane.
            height = heightLevels.Last();
            currentHeightLevel = heightLevels.Count - 1;
        }
        else
        {
            int? newHeightLevel = null;

            for (int i = 0; i < heightLevels.Count; i++)
            {
                if (height <= heightLevels[i])
                {
                    newHeightLevel = i;
                    break;
                }
            }


            currentHeightLevel = newHeightLevel ?? heightLevels.Count - 1;
        }


        if (currentHeightLevel != oldHeightLevel)
        {
           // Debug.Log(currentHeightLevel);
            material.SetTexture(textureID, groundTextures[currentHeightLevel]);
        }
    }


}
