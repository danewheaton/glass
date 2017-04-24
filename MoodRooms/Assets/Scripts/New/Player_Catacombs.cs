using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_Catacombs : MonoBehaviour
{
    [SerializeField]
    GameObject gabe, gabeTrigger, disappearingWall, end;

    bool facingGabe, inTrigger;

    private void Update()
    {
        print(inTrigger);

        facingGabe = Vector3.Angle(gabe.transform.position, transform.forward) >= 60;

        if (facingGabe && inTrigger) disappearingWall.SetActive(false);
        else disappearingWall.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gabeTrigger && !facingGabe)
        {
            gabe.SetActive(true);
            inTrigger = true;
        }

        if (other.gameObject == end) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gabeTrigger)
        {
            inTrigger = false;
        }
    }
}
