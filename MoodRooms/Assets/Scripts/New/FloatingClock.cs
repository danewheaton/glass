using UnityEngine;
using System.Collections;

public class FloatingClock : MonoBehaviour
{
    public float timeMultiplier = 1, backwardSpeed = 0;

    TextMesh timeText;
    float minutes = 41, hours = 9;

    void Start()
    {
        timeText = GetComponent<TextMesh>();
    }

    void Update()
    {
        UpdateClock();
        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position -= new Vector3(0, 0, backwardSpeed);
    }

    void UpdateClock()
    {
        if (minutes < 10) timeText.text = hours + ":0" + (int)minutes;
        else timeText.text = hours + ":" + (int)minutes;

        minutes += (timeMultiplier * Time.deltaTime);
        if (minutes >= 59.9999999f)
        {
            hours++;
            minutes = 0;
        }
        if (hours > 12) hours = 1;
    }
}
