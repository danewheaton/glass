using UnityEngine;
using System.Collections;

public class Player_Refectory : MonoBehaviour
{
    [SerializeField]
    GameObject first, second, third, fourth, fifth;

    [SerializeField]
    FloatingClock clock;

    vp_FPController controller;

    void Start()
    {
        Physics.gravity = new Vector3(0, -.2f, 0);
        controller = GetComponent<vp_FPController>();
    }

    void OnCollisionEnter(Collision other)
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == first)
        {
            if (controller.MotorAcceleration >= .02f) controller.MotorAcceleration -= .02f;
            clock.backwardSpeed += .03f;
            clock.timeMultiplier += .25f;
        }
        else if (other.gameObject == second)
        {
            if (controller.MotorAcceleration >= .02f) controller.MotorAcceleration -= .015f;
            clock.timeMultiplier += 2;
        }
        else if (other.gameObject == third)
        {
            if (controller.MotorAcceleration >= .02f) controller.MotorAcceleration -= .015f;
            clock.timeMultiplier += 5;
        }
        else if (other.gameObject == fourth)
        {
            if (controller.MotorAcceleration >= .02f) controller.MotorAcceleration -= .01f;
            clock.timeMultiplier += 10;
        }
        else if (other.gameObject == fifth)
        {
            controller.MotorAcceleration = 0;
            StartCoroutine(FindObjectOfType<Credits>().FadeOut());
        }
    }
}
