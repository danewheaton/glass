﻿using UnityEngine;
using System.Collections;

public class OpenSurveyOnQuit : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        //if (!Application.isEditor) Application.OpenURL("https://goo.gl/forms/tEhPXoeElC5M0tNK2");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
