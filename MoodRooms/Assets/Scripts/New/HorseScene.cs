using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HorseScene : MonoBehaviour
{
    [SerializeField]
    AudioSource music;

    [SerializeField]
    AudioSource alarm;

    [SerializeField]
    Text alarmText;

    [SerializeField]
    Image panelImage;

    [SerializeField]
    GameObject snoozeButton, horse;

    void Start()
    {
        StartCoroutine(Horse());
        StartCoroutine(Music());
    }

    public void OnClickSnoozeButton()
    {
        vp_Utility.LockCursor = !vp_Utility.LockCursor;
        alarmText.gameObject.SetActive(false);
        alarm.Stop();
        snoozeButton.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Horse()
    {
        yield return new WaitForSeconds(2);

        float timer = 10;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            panelImage.color = Color.Lerp(Color.black, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Music()
    {
        yield return new WaitForSeconds(.01f);
        vp_Utility.LockCursor = !vp_Utility.LockCursor;
        music.volume = 0;

        float timer = music.clip.length;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            music.volume = Mathf.Lerp(0, 1, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        alarmText.gameObject.SetActive(true);
        vp_Utility.LockCursor = !vp_Utility.LockCursor;
        Cursor.visible = true;
        music.Stop();
        alarm.Play();
        snoozeButton.SetActive(true);
        horse.SetActive(false);
    }
}
