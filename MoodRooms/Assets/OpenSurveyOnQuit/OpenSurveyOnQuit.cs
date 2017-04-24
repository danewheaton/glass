using UnityEngine;
using System.Collections;

public class OpenSurveyOnQuit : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        Application.OpenURL("https://goo.gl/forms/tEhPXoeElC5M0tNK2");
    }
}
