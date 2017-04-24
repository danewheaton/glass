using UnityEngine;
using System.Collections;

public class Dummy_Dept : MonoBehaviour 
{
    [SerializeField] Transform playerTransform;
    [SerializeField] int dummyNumber;

    Vector3 dummyPos;

    void Update()
    {
        Vector3 playerPos = playerTransform.position;

        switch (dummyNumber)
        {
            case 1:
                dummyPos = new Vector3(playerPos.x, playerPos.y, playerPos.z - 8.54f);
                break;
            case 2:
                dummyPos = new Vector3(playerPos.x, playerPos.y, playerPos.z + 8.54f);
                break;
        }

        transform.position = dummyPos;
    }
}
