using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{

    public static MainMenuController mainMenuController;
    public string sceneToLoad;

    private void Awake()
    {
        mainMenuController = this;

    }

    public void ChangeScene(string sceneName)
    {
        sceneToLoad = sceneName;

        if (sceneToLoad != null)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
