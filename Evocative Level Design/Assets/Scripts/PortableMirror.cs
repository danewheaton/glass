using UnityEngine;
using System.Collections;

public class PortableMirror : MonoBehaviour
{
    [SerializeField]
    GameObject mirror;

    [SerializeField]
    float rotationSpeed = 2;

    void Update()
    {
        if (Input.GetButton("Jump"))
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);

        if (Input.GetButtonDown("Cancel"))
        {
            mirror.transform.eulerAngles = new Vector3(mirror.transform.eulerAngles.x, mirror.transform.eulerAngles.y - 180, mirror.transform.eulerAngles.z);
        }
    }
}
