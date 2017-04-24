using UnityEngine;
using System.Collections;

public class PortableShard : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
