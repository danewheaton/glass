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

    Image panelImage;
    GameObject player;
    Color originalColor;
    bool clicked, canFreezeTime;

    void Start()
    {
        panelImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        originalColor = textColor.color;

        StartCoroutine(FadeIn());
    }

    void Update()
    {
        print(Time.timeScale);

        if (Input.GetKeyDown(KeyCode.Mouse0)) clicked = true;

        if (Won) StartCoroutine(FadeOut());

        if (canFreezeTime && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
            creditsText.color = Time.timeScale == 1 ? textColor.color : Color.white;
        }
    }

    IEnumerator FadeIn()
    {
        float timer = 1;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            panelImage.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        panelImage.color = Color.clear;

        while (!clicked) yield return null;

        elapsedTime = 0;
        while (elapsedTime < timer)
        {
            titleScreenPanel.color = Color.Lerp(Color.white, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        titleScreenPanel.color = Color.clear;

    }

    IEnumerator FadeOut()
    {
        Won = false;
        canFreezeTime = true;

        player.GetComponent<vp_FPController>().MotorAcceleration = 0;
        flamingoAmbush.SetActive(true);
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(adequateAmountOfTimeToTakeAGoodLongGanderAtTheToughGangOfMercilessAviansSurroundingYou);

        panelImage.color = Color.black;
        creditsText.color = textColor.color;

        yield return new WaitForSeconds(2);
        creditsText.text = "EVOCATIVE LEVEL DESIGN (not actually the name, there is no name)";
        yield return new WaitForSeconds(2.5f);
        creditsText.text = "";
        yield return new WaitForSeconds(1.5f);
        creditsText.text = "ART                 Joakim Saldamando\n\nDESIGN         Aidan Walsh\n                          Dane Wheaton\n\nMUSIC           Aidan Walsh";
        yield return new WaitForSeconds(4);
        creditsText.text = "";
        yield return new WaitForSeconds(3);
        creditsText.text = "If you're interested in the story possibilities of Evocative Level Design (not the real name), the next two slides go through an example of the kind of narrative we believe this sort of gameplay lends itself well to.\n\nFeel free to press Esc any time to exit the application, or spacebar to freeze / unfreeze time. Be warned: more text ahead, as well as a great big tonal shift.\n\n\nReady?";
        yield return new WaitForSeconds(12);
        creditsText.text = "Two people crossed paths many years ago. The encounter, whose exact nature is left to the player's imagination, was a crisis. The trajectories of both lives were altered forever, and while neither party has seen the other in the twenty-plus years since, the emotional baggage from their brief interaction is such that they can't help but think about each other every day.\n\nWithin this example, the characters' complex relationship is displayed in-game by a series of short vignettes - probably no more than four or five, for a total play-time comparable in length to this demo. In each of these short segments, the player controls one character or the other.\n\nWhen the player encounters a mirror, they do not see a reflection of the character they are playing as or the room they are standing in. They see the other character, in a very different space which happens to share some similarities in layout (for example, a dentist's lobby and an overgrown shed are both places you might find a table piled with magazines).";
        yield return new WaitForSeconds(5);
        creditsText.text = "The character in the mirror wants something or is trying to do something, or is otherwise struggling - this can be communicated through animation, voice acting, or a static tableau. Gameplay consists of navigating impossible spaces and simple environmental puzzles which draw inspiration from places in these two characters' lives - both grand and mundane - as the player indirectly interacts with the other character through the space's mirrors. These interactions are either malicious or helpful, depending on the context.\n\nThe interactions escalate quickly in emotional intensity. In the final area, items from both characters' lives share a common space, much like the chairs and fountain in the cathedral area in this demo. The player discovers that the characters' motives are not what they seemed at first, and neither are the kind of person the player thought they were. The game ends abruptly as the player rounds a corner and comes face-to-face with the other character. There are no scene transitions or breaks in gameplay, which means the player only knows they've switched characters when they see the other character in a mirror.\n\nThat's it! Thank you for reading, and of course thank you for playing!";
        yield return new WaitForSeconds(5);
        Application.Quit();
        //SceneManager.LoadScene(0);
    }

    public IEnumerator FlashWhite()
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
