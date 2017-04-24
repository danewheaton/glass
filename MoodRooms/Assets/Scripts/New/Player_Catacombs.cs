using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_Catacombs : MonoBehaviour
{
    [SerializeField]
    GameObject gabe, gabeTrigger, disappearingWall, end;

    [SerializeField]
    AudioSource jumpscare, music;

    bool facingGabe, inTrigger, hasPlayedJumpscare, highEnough;

    private void Update()
    {
        print(transform.position.y);

        facingGabe = Vector3.Angle(gabe.transform.position, transform.forward) >= 60;

        if (facingGabe && inTrigger)
        {
            disappearingWall.SetActive(false);
            if (!hasPlayedJumpscare && Vector3.Angle(gabe.transform.position, transform.forward) >= 30)
            {
                jumpscare.Play();
                hasPlayedJumpscare = true;
            }
        }
        else disappearingWall.SetActive(true);


        if (transform.position.y > -4 && !highEnough)
            StartCoroutine(FadeMusicOut());
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

    IEnumerator FadeMusicOut()
    {
        highEnough = true;

        float elapsedTime = 0;
        float timer = 4;
        while (elapsedTime < timer)
        {
            music.volume = Mathf.Lerp(1, 0, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
