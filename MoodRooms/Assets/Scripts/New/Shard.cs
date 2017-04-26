using UnityEngine;
using System.Collections;

public class Shard : MonoBehaviour
{
    [SerializeField]
    float bobHeight = .4f, moveSpeed = .2f, turnSpeed = 100;

    Vector3 originalPos, targetPos, highYPos, lowYPos;

    private void Start()
    {
        originalPos = transform.position;
        highYPos = new Vector3(transform.position.x, transform.position.y + bobHeight, transform.position.z);
        lowYPos = new Vector3(transform.position.x, transform.position.y - bobHeight, transform.position.z);

        int randomInt = Random.Range(0, 2);
        targetPos = randomInt == 0 ? highYPos : lowYPos;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < (bobHeight / 2))
            targetPos = (targetPos == highYPos ? lowYPos : highYPos);
    }
}
