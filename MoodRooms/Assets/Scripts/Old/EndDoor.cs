using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndDoor : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        SceneManager.LoadScene(0);
    }
}
