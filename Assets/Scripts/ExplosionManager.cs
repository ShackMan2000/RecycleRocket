using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{




    //make it more of a manager, create a bunch and update their rotation. Maybe some kind of callback that when an explosion is done, it's going to be removed



    [SerializeField]
    private int explosionCount;

    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private Camera vrCam;

    private List<Transform> activeExplosions;



    private bool explosionIsHappening;



    private void OnEnable()
    {
        RocketPhysics.EvtRocketExploded += CreateExplosion;
    }



    public void CreateExplosion(Vector3 impactLocation)
    {
        if (explosionIsHappening)
        {
            print("already exploding");
            return;
        }
        else
        {
            explosionIsHappening = true;
            activeExplosions = new List<Transform>();

            for (int i = 0; i < explosionCount; i++)
            {
                GameObject explosion = Instantiate(explosionPrefab, transform);

                Vector3 position = impactLocation + Random.insideUnitSphere * explosionRadius;

                explosion.transform.position = position;

                activeExplosions.Add(explosion.transform);
            }



        }

    }


    void Update()
    {
        if (activeExplosions != null && activeExplosions.Count > 0)
        {
            Vector3 targetDirection = vrCam.transform.position - transform.position;
            //transform.rotation = Camera.main.transform.rotation;

            foreach (var explosion in activeExplosions)
            {
                explosion.transform.rotation = Quaternion.LookRotation(targetDirection);
            }         
        }
    }







    private void OnDisable()
    {
        RocketPhysics.EvtRocketExploded -= CreateExplosion;
    }




}






