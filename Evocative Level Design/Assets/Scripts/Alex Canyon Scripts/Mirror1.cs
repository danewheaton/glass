using UnityEngine;
using System.Collections;

public class Mirror1 : MonoBehaviour {

    public Transform playerLoc;
    Quaternion playerRotation;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = playerLoc.position;
            ///rotating code that doesnt work, need to turn player 180 degrees after teleporting through the first mirror
            //playerRotation = Quaternion.LookRotation(-other.transform.forward, Vector3.forward);

            //other.transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, 5 * Time.deltaTime);
        }
    }
}
