using UnityEngine;
using System.Collections;

public class Player_Refectory : MonoBehaviour
{
    [SerializeField]
    Collider first, second, third, fourth, fifth;

    FloatingClock clock;
    vp_FPController controller;

    void Start()
    {
        Physics.gravity = new Vector3(0, -.2f, 0);
        clock = FindObjectOfType<FloatingClock>();
        controller = GetComponent<vp_FPController>();
    }

    void OnCollisionEnter(Collision other)
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (controller.MotorAcceleration >= .1f)
        {
            if (other == first)
            {
                controller.MotorAcceleration -= .1f;
                clock.backwardSpeed += .01f;
                clock.timeMultiplier += .1f;
            }
            else if (other == second)
            {
                controller.MotorAcceleration -= .1f;
                clock.timeMultiplier += .1f;
            }
            else if (other == second)
            {
                controller.MotorAcceleration -= .1f;
                clock.timeMultiplier += .1f;
            }
            else if (other == second)
            {
                controller.MotorAcceleration -= .1f;
                clock.timeMultiplier += .1f;
            }
            else if (other == second)
            {
                controller.MotorAcceleration -= .1f;
                clock.timeMultiplier += .1f;
            }
        }
    }
}
