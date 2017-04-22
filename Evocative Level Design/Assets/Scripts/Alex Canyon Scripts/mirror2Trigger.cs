using UnityEngine;
using System.Collections;

public class mirror2Trigger : MonoBehaviour {

    //this script attached to mirror3's camera

    public Transform playerLoc;

	void OnTriggerEnter(Collider other)
    {
        //I think it would be cool if the player had to look at the mirror's camera orgin point (this camer) then teleport and not realize it until they move
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = playerLoc.position;
            //other.transform.Rotate(new Vector3())
        }
    }
}
