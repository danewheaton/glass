using UnityEngine;
using System.Collections;

public class Player_Refectory : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer myTime;

    [SerializeField]
    Sprite nineFortyOne;

    [SerializeField]
    GameObject first, second, third, fourth, fifth;

    [SerializeField]
    FloatingClock clock;

    vp_FPController controller;

    Sprite originalSprite;

    void Start()
    {
        Physics.gravity = new Vector3(0, -.2f, 0);
        controller = GetComponent<vp_FPController>();
        originalSprite = myTime.sprite;
    }

    void OnCollisionEnter(Collision other)
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == first)
        {
            print("uno");
            controller.MotorAcceleration = .06f;
            clock.backwardSpeed += .04f;
            clock.timeMultiplier += .25f;
        }
        else if (other.gameObject == second)
        {
            print("dos");
            controller.MotorAcceleration = .04f;
            clock.timeMultiplier += 1;
        }
        else if (other.gameObject == third)
        {
            print("tres");
            controller.MotorAcceleration = .025f;
            clock.timeMultiplier += 5;
        }
        else if (other.gameObject == fourth)
        {
            print("quatro");
            controller.MotorAcceleration = .0125f;
            clock.timeMultiplier += 10;
        }
        else if (other.gameObject == fifth)
        {
            print("cinco");
            controller.MotorAcceleration = .005f;
            StartCoroutine(FindObjectOfType<Credits>().FadeOut());
        }
    }

    public void ChangeSprite (bool toNineFortyOne)
    {
        myTime.sprite = toNineFortyOne ? nineFortyOne : null;
    }
}
