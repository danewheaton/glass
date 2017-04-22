using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_Corridor : MonoBehaviour
{
    [SerializeField] Material materialToChange;
    [SerializeField] GameObject[] rooms;
    [SerializeField] AnimationCurve speedCurve = new AnimationCurve(new Keyframe(0, 2), new Keyframe(50, 50));

    NonUFPSPlayerController controller;
    float originalSpeed;
    int counter;
    int speedCharge;

    void Start()
    {
        materialToChange.color = new Color(.6f, .6f, .3f);
        controller = GetComponent<NonUFPSPlayerController>();
        originalSpeed = controller.movementSpeed;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") <= 0) speedCharge = 0;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Mirror1" &&
            Vector3.Angle(other.transform.position - transform.position, transform.forward) <= Camera.main.fieldOfView)
        {
            transform.Rotate(Vector3.up * 180);
            transform.position += new Vector3(10, 0, 0);
            controller.movementSpeed = speedCurve.Evaluate(speedCharge);
            if (controller.movementSpeed >= 20) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            counter++;
            speedCharge += 2;
            if (counter < rooms.Length) rooms[counter].SetActive(true);
            rooms[counter - 1].SetActive(false);

            Color newColor = new Color(materialToChange.color.r + .02f, materialToChange.color.g - .02f, materialToChange.color.b - .02f);
            materialToChange.color = newColor;
        }
    }
}
