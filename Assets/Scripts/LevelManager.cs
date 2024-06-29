using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager levelManager;
    public string sceneToLoad;

    private void Awake()
    {
        levelManager = this;

    }

    public void ChangeScene(string sceneName)
    {
        sceneToLoad = sceneName;

        if (sceneToLoad != null)
        {
            SceneManager.LoadScene(sceneToLoad);

            if(Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }

}
