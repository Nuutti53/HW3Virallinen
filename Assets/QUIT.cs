using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class QUIT : MonoBehaviour
{
    public InputActionReference action;
    public InputActionReference action2;

    // Start is called before the first frame update
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) => {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        };

        action2.action.Enable();
        action2.action.performed += (ctx) => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
