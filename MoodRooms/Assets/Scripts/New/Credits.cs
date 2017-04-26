using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour
{
    public static bool Won;
    public Image titleScreenPanel;
    public Text creditsText, rewardText;
    public GameObject flamingoAmbush;
    public float titleScreenFadetime = 1.5f;
	public float flashFadeTime = 2f;
	public SoundFeedback soundFeed;
    public Material textColor;

    Image panelImage;
    GameObject player;
    Color originalColor;
    bool canClick, clicked, canFreezeTime;

    void Start()
    {
        panelImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (textColor != null) originalColor = textColor.color;
        
        if (SceneManager.GetActiveScene().name == "01") StartCoroutine(StartGame());
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
    
    IEnumerator StartGame()
    {
        titleScreenPanel.color = Color.black;

        yield return new WaitForSeconds(3);

        GameObject.FindGameObjectWithTag("Player").GetComponent<vp_FPController>().enabled = true;

        float timer = 6;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            titleScreenPanel.color = Color.Lerp(Color.black, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        titleScreenPanel.color = Color.clear;

        yield return new WaitForSeconds(1);
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
        float timer = 5;
        while (elapsedTime < timer)
        {
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = Mathf.Lerp(1, 0, elapsedTime / timer);
            panelImage.color = Color.Lerp(Color.clear, Color.black, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume = 0;
        panelImage.color = Color.black;
        
        GetComponent<AudioSource>().Play();

        panelImage.color = Color.black;
        creditsText.color = Color.clear;
        creditsText.text = "                by                                              \n\n\n\n\n\nJames Robertson          \n\n\n               Dane Wheaton";

        yield return new WaitForSeconds(5);

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
        
        yield return new WaitForSeconds(2.5f);
        creditsText.text = "original music                                      \n\n               by                           \n\n\n\n\n                               Matt Sullivan";

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

        yield return new WaitForSeconds(2.5f);
        creditsText.text = "                               original 3D art   \n\n          by                           \n\n\n\n\nJoakim Saldamando";

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

    public IEnumerator TurnScreenRed()
    {
        soundFeed.Vwoop();
        panelImage.color = Color.red;

        yield return new WaitForEndOfFrame();
    }

    public IEnumerator TurnScreenWhite()
    {
        soundFeed.Vwoop();
        panelImage.color = Color.white;

        yield return new WaitForEndOfFrame();
    }

    public IEnumerator FlashWhite()
    {
        soundFeed.Vwoop();
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

    public IEnumerator FlashRewardText(int counter)
    {
        soundFeed.Vwoop();

        rewardText.text = "+ " + counter + " seconds";

        float timer = .1f;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            rewardText.color = Color.Lerp(Color.clear, Color.white, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        rewardText.color = Color.white;

        timer = flashFadeTime;
        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            rewardText.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        rewardText.color = Color.clear;
    }
}
