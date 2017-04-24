using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenMirror : MonoBehaviour {

    public Slider slider;

    [SerializeField]

    GameObject Mirror;

    [SerializeField]

    GameObject Wall;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (slider.value > 0.85)
        {
           // Mirror.GetComponent<MeshCollider>();
           
            Mirror.SetActive(false);
            Wall.SetActive(false);
        }
	
	}
}
