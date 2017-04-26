using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum WhichMirror { FIRST, SECOND, THIRD, FOURTH, FIFTH }

public class Player_Catacombs : MonoBehaviour
{
    [SerializeField]
    GameObject gabe, gabeTrigger, disappearingWall, end, mirror1, mirror2, mirror3, mirror4, mirror5, doorblocker;

    [SerializeField]
    AudioSource jumpscare, music, doorCreak;

    GameObject mirror;

    WhichMirror whichMirror = WhichMirror.FIRST;

    bool facingGabe, inTrigger, hasPlayedJumpscare, highEnough, facingMirror;

    void Start()
    {
        Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | ((1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("UI"))));

        mirror = mirror1;
    }

    private void Update()
    {
        //print(Vector3.Distance(transform.position, mirror.transform.position));
        //print(Vector3.Angle(mirror.transform.position, -transform.forward));
        
        facingMirror = Vector3.Angle(mirror.transform.position, -transform.forward) <= 90 && Vector3.Distance(transform.position, mirror.transform.position) < 3;

        if (facingMirror)
        {
            switch (whichMirror)
            {
                case WhichMirror.FIRST:
                    Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | ((1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Glass1"))));
                    mirror = mirror2;
                    whichMirror = WhichMirror.SECOND;
                    break;
                case WhichMirror.SECOND:
                    Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | ((1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Glass1")) | (1 << LayerMask.NameToLayer("Door01"))));
                    mirror = mirror3;
                    whichMirror = WhichMirror.THIRD;
                    break;
                case WhichMirror.THIRD:
                    Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | ((1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Glass1")) | (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door02"))));
                    mirror = mirror4;
                    whichMirror = WhichMirror.FOURTH;
                    break;
                case WhichMirror.FOURTH:
                    Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | ((1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Glass1")) | (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door02")) |
                    (1 << LayerMask.NameToLayer("Portal01"))));
                    mirror = mirror5;
                    whichMirror = WhichMirror.FIFTH;
                    break;
                case WhichMirror.FIFTH:
                    Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | ((1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Glass1")) | (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door02")) |
                    (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("Glass2"))));
                    doorblocker.SetActive(false);
                    doorCreak.Play();
                    break;
                default:
                    break;
            }
        }

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
