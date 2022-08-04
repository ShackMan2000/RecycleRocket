using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hacker : MonoBehaviour
{

    // stuff like insta reload scene



 

    private void OnEnable()
    {
        print("WARNING, hacker is in the scene");



        
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ReloadScene();
    }



    private void ReloadScene()
    {
        print("reload");
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }






}
