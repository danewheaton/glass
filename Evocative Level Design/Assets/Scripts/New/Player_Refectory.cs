using UnityEngine;
using System.Collections;

public class Player_Refectory : MonoBehaviour
{
    void Start()
    {
        Physics.gravity = new Vector3(0, -.5f, 0);
    }

    void OnCollisionEnter(Collision other)
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }
}
