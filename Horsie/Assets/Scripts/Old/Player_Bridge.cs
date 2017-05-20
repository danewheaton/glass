using UnityEngine;
using System.Collections;

public class Player_Bridge : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FallTrigger")
        {
            transform.position = spawnPoint.position;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
