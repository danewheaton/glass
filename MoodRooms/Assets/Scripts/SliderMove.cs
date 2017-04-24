using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderMove : MonoBehaviour {

    public float PositionX;
    public float PositionZ;

    [SerializeField]
    float maxDistanceToActivate = 4;

    [SerializeField]
    LayerMask layerToCheckForObjects;

    [SerializeField]
    GameObject HandIcon;

    [SerializeField]
    GameObject DragImage;

    public Slider slider;


    
    void Start () {
        Vector3 endPoint = (transform.forward * maxDistanceToActivate) + transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Debug.DrawLine(transform.position, endPoint, Color.red);


    }
	
	
	void Update () {

    }


    void FixedUpdate()
    {
        RaycastHit raycastHit;

        Vector3 endPoint = (transform.forward * maxDistanceToActivate) + transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(transform.position, transform.forward,
           out raycastHit, maxDistanceToActivate, layerToCheckForObjects))
        {
            Debug.Log("Ray hit");
            HandIcon.SetActive(true);
            DragImage.SetActive(false);


            if (Input.GetMouseButton(0))
            {
                PositionX = ray.direction.x;

                slider.value = PositionX;
                
                Debug.Log("Holding left Mouse");
                DragImage.SetActive(true);
                HandIcon.SetActive(false);

            }





            if (Input.GetMouseButton(1))
            {
                

                slider.value = PositionZ;
                
                Debug.Log("Holding right Mouse");
                DragImage.SetActive(true);
                HandIcon.SetActive(false);

            }
        }

        if (!(Physics.Raycast(transform.position, transform.forward,
           out raycastHit, maxDistanceToActivate, layerToCheckForObjects)))
        { HandIcon.SetActive(false);
            DragImage.SetActive(false);
        }

        PositionZ = ray.direction.z;

        Debug.DrawLine(transform.position, endPoint, Color.red);


    }
}
