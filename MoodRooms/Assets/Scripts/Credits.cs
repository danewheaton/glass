using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour
{
    public static bool Won;
    public Image titleScreenPanel;
    public Text creditsText;
    public GameObject flamingoAmbush;
    public float titleScreenFadetime = 1.5f;
	public float flashFadeTime = 2f;
    public float adequateAmountOfTimeToTakeAGoodLongGanderAtTheToughGangOfMercilessAviansSurroundingYou = 2;
	public SoundFeedback soundFeed;
    public Material textColor;

    #region new variables for intro sequence

    [SerializeField]
    AudioSource music;

    [SerializeField]
    AudioSource alarm;

    [SerializeField]
    AudioClip alarmSound;

    [SerializeField]
    Text alarmText;

    #endregion

    Image panelImage;
    GameObject player;
    Color originalColor;
    bool canClick, clicked, canFreezeTime;

    void Start()
    {
        panelImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        originalColor = textColor.color;
        
        if (SceneManager.GetActiveScene().name == "01") StartCoroutine(FadeIn());
        if (SceneManager.GetActiveScene().name == "Refectory") StartCoroutine(RefectoryFadeIn());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canClick) clicked = true;

        if (Won) StartCoroutine(FadeOut());

        if (canFreezeTime && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            creditsText.color = Time.timeScale == 1 ? textColor.color : Color.white;
        }
    }

    IEnumerator FadeIn()
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
        canClick = true;
        alarm.Play();


        while (!clicked) yield return null;

        vp_Utility.LockCursor = !vp_Utility.LockCursor;
        alarmText.gameObject.SetActive(false);
        alarm.Stop();

        yield return new WaitForSeconds(1);

        GameObject.FindGameObjectWithTag("Player").GetComponent<vp_FPController>().enabled = true;

        timer = 4;
        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            titleScreenPanel.color = Color.Lerp(Color.black, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        titleScreenPanel.color = Color.clear;

        yield return new WaitForSeconds(1);

        DynamicMusic dm = FindObjectOfType<DynamicMusic>();
        StartCoroutine(dm.ToggleTrack(true, "bass"));

    }

    IEnumerator RefectoryFadeIn()
    {
        panelImage.color = Color.white;

        float timer = 5;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            panelImage.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panelImage.color = Color.clear;
    }

    public IEnumerator FadeOut()
    {
        Won = false;
        canFreezeTime = true;

        float elapsedTime = 0;
        float timer = 2;
        while (elapsedTime < timer)
        {
            foreach (AudioSource a in FindObjectsOfType<AudioSource>()) a.volume = Mathf.Lerp(a.volume, 0, elapsedTime / timer / 4);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        GetComponent<AudioSource>().volume = 1;
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(10);

        elapsedTime = 0;
        timer = 2;
        while (elapsedTime < timer)
        {
            panelImage.color = Color.Lerp(Color.clear, Color.black, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //yield return new WaitForSeconds(1);


        panelImage.color = Color.black;
        creditsText.color = Color.clear;
        creditsText.text = "                by                                              \n\n\n\n\n\nJames Robertson          \n\n\n               Dane Wheaton";

        yield return new WaitForSeconds(4);

        elapsedTime = 0;
        timer = 4;
        while (elapsedTime < timer)
        {
            creditsText.color = Color.Lerp(Color.clear, textColor.color, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2.5f);

        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            creditsText.color = Color.Lerp(textColor.color, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2.5f);
        creditsText.text = "                     based on the game\n\n\n\n\nHealthy Breakfast                                                                  \n\n\nby                                   \n\nJoakim Saldamando                                                      \n\n\nAidan Walsh                           \n    Dane Wheaton";

        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            creditsText.color = Color.Lerp(Color.clear, textColor.color, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(2.5f);

        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            creditsText.color = Color.Lerp(textColor.color, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(4);
        creditsText.text = "Thank you for playing!";

        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            creditsText.color = Color.Lerp(Color.clear, textColor.color, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(5);

        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            creditsText.color = Color.Lerp(textColor.color, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(5);
        Application.Quit();
        //SceneManager.LoadScene(0);
    }

    public IEnumerator FlashRandomColor()
    {
		soundFeed.Vwoop ();

		Color[] randomColors = new Color[7];
		randomColors[0] = Color.cyan;
        randomColors[1] = Color.cyan;
		randomColors[2] = Color.magenta;
        randomColors[3] = Color.magenta;
        randomColors[4] = Color.red;
        randomColors[5] = Color.white;
        randomColors[6] = Color.yellow;

		Color newColor = randomColors[Random.Range(0, 6)];
		panelImage.color = newColor;

		float timer = flashFadeTime;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
			panelImage.color = Color.Lerp(newColor, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panelImage.color = Color.clear;
    }

    public IEnumerator FlashRed()
    {
        soundFeed.Vwoop();
        panelImage.color = Color.red;

        yield return new WaitForEndOfFrame();
    }

    public IEnumerator FlashWhite2()
    {
        panelImage.color = Color.white;

        float timer = flashFadeTime;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            panelImage.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panelImage.color = Color.clear;
    }
}
