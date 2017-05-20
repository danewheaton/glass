using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public enum GameStates { START, GREEN_PILLARS }

public class Player_Sphere : MonoBehaviour
{
    [SerializeField]
    GameObject fakeInnerSphere, fakeOuterSphere, finalCube, middlePillar, ramp01;

    [SerializeField]
    GameObject[] twoMorePillars;

    [SerializeField]
    Material white, green, gold;

    GameStates currentState;

    List<GameObject> greenPillars = new List<GameObject>();
    int greenCubesCollected;

    void Update()
    {
        switch (currentState)
        {
            case GameStates.START:
                if (greenPillars.Count >= 3)
                {
                    foreach (GameObject g in twoMorePillars) g.SetActive(true);
                    ramp01.SetActive(true);
                    middlePillar.SetActive(false);
                    currentState = GameStates.GREEN_PILLARS;
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "GreenCube":
                other.gameObject.SetActive(false);
                greenCubesCollected++;

                switch (greenCubesCollected)
                {
                    case 3:
                        fakeOuterSphere.SetActive(false);
                        break;
                    case 4:
                        fakeInnerSphere.SetActive(false);
                        break;
                }
                break;
            case "InvisiblePillar":
                other.gameObject.GetComponent<Renderer>().material = white;
                other.isTrigger = false;
                greenPillars.Add(other.gameObject);
                break;
        }

        if (other.gameObject == finalCube) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == twoMorePillars[1])
        {
            twoMorePillars[1].GetComponent<Renderer>().material = white;
            middlePillar.SetActive(true);
            middlePillar.GetComponent<Renderer>().material = gold;
            finalCube.GetComponent<Renderer>().material = white;
        }
    }
}
