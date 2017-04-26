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
    GameObject[] horses;

    [SerializeField]
    FloatingClock clock;

    vp_FPController controller;

    Sprite originalSprite;

    void Start()
    {
        Physics.gravity = new Vector3(0, -.2f, 0);
        controller = GetComponent<vp_FPController>();
        originalSprite = myTime.sprite;

        StartCoroutine(ActivateHorses());
    }

    void OnCollisionEnter(Collision other)
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == first)
        {
            //StartCoroutine(FindObjectOfType<Credits>().AlarmStart());
            controller.MotorAcceleration = .06f;
            clock.backwardSpeed += .04f;
            clock.timeMultiplier += .25f;
        }
        else if (other.gameObject == second)
        {
            controller.MotorAcceleration = .04f;
            clock.timeMultiplier += 1;
        }
        else if (other.gameObject == third)
        {
            controller.MotorAcceleration = .025f;
            clock.timeMultiplier += 5;
        }
        else if (other.gameObject == fourth)
        {
            controller.MotorAcceleration = .0125f;
            clock.timeMultiplier += 10;
        }
        else if (other.gameObject == fifth)
        {
            controller.MotorAcceleration = .005f;
            StartCoroutine(FindObjectOfType<Credits>().FadeOut());
            StopCoroutine(FindObjectOfType<Credits>().AlarmStart());
        }
    }

    public void ChangeSprite (bool toNineFortyOne)
    {
        myTime.sprite = toNineFortyOne ? nineFortyOne : null;
    }

    IEnumerator ActivateHorses()
    {
        foreach(GameObject g in horses)
        {
            yield return new WaitForSeconds(Random.Range(0f, 5f));
            g.SetActive(true);
        }
    }
}
