using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //takes care of all things related to levels. 



    // 



    [SerializeField]
    private Rigidbody rocketRigidbody;


    [SerializeField]
    private float maxRandomRotation;




    
    public void GiveRocketRandomRotation()
    {
        Vector3 newRotation = new Vector3(Random.Range(-maxRandomRotation, maxRandomRotation), Random.Range(-maxRandomRotation, maxRandomRotation), Random.Range(-maxRandomRotation, maxRandomRotation));


        rocketRigidbody.transform.Rotate(newRotation);


    }



    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
            GiveRocketRandomRotation();
    }

}
