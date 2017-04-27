using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_Corridor : MonoBehaviour
{
    public Material materialToChange;
    [SerializeField] GameObject[] rooms;
    [SerializeField] AnimationCurve speedCurve = new AnimationCurve(new Keyframe(0, 2), new Keyframe(50, 50));
    [SerializeField] AudioSource music;
    [SerializeField] Font[] fonts;
    [SerializeField] Visualizer_Size timeText, amtext;

    Color[] colors = new Color[12];
    NonUFPSPlayerController controller;
    float originalSpeed;
    int counter, speedCharge;
    bool startedSinging;

    void Start()
    {
        materialToChange.color = new Color(.3f, .3f, .3f);
        controller = GetComponent<NonUFPSPlayerController>();
        originalSpeed = controller.movementSpeed;

        colors[0] = Color.black;
        colors[1] = Color.black;
        colors[2] = Color.gray;
        colors[3] = Color.black;
        colors[4] = Color.gray;
        colors[5] = Color.red;
        colors[6] = Color.grey;
        colors[7] = Color.grey;
        colors[8] = Color.red;
        colors[9] = Color.gray;
        colors[10] = Color.grey;
        colors[11] = Color.white;

        Invoke("SetStartedSinging", 19);
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") <= 0) speedCharge = 0;

        if (Input.GetKeyDown(KeyCode.Escape)) materialToChange.color = new Color(.3f, .3f, .3f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Mirror1")
        {
            transform.Rotate(Vector3.up * 180);
            transform.position += new Vector3(10, 0, 0);
            controller.movementSpeed = speedCurve.Evaluate(speedCharge);
            if (controller.movementSpeed >= 20) StartCoroutine(ChangeScene());
            counter++;
            speedCharge += 2;
            if (counter < rooms.Length) rooms[counter].SetActive(true);
            rooms[counter - 1].SetActive(false);

            Color newWallColor = new Color(materialToChange.color.r + .02f, materialToChange.color.g - .02f, materialToChange.color.b - .02f);
            materialToChange.color = newWallColor;

            if (counter > 5)
            {
                timeText.CrankItX = Random.Range(.05f, .2f);
                timeText.CrankItY = Random.Range(.05f, .2f);
                timeText.CrankItZ = Random.Range(.05f, .2f);
                amtext.CrankItX = Random.Range(.75f, 3f);
                amtext.CrankItY = Random.Range(.75f, 3f);
                amtext.CrankItZ = Random.Range(.75f, 3f);

                
                timeText.GetComponent<TextMesh>().fontStyle = (FontStyle)Random.Range(0, 4);
                amtext.GetComponent<TextMesh>().fontStyle = (FontStyle)Random.Range(0, 4);
            }
            if (counter == 5)
            {
                timeText.GetComponent<TextMesh>().text = "7:30";
            }
            if (counter == 10)
            {
                timeText.GetComponent<TextMesh>().text = "7:31";
            }
            if (counter == 20)
            {
                timeText.GetComponent<TextMesh>().text = "7:32";
            }
            if (counter == 30)
            {
                timeText.GetComponent<TextMesh>().text = "7:33";
            }
            if (counter > 40)
            {
                timeText.GetComponent<TextMesh>().font = fonts[Random.Range(0, fonts.Length)];
            }

            if (startedSinging)
            {
                Color newTextColor = colors[Random.Range(0, colors.Length)];
                timeText.GetComponent<TextMesh>().color = newTextColor;
                amtext.GetComponent<TextMesh>().color = newTextColor;

                if (counter > 20) StartCoroutine(TwistItAllAround());
            }
        }

        if (other.gameObject.tag == "Pen" || other.gameObject.tag == "Finish")
        {
            StartCoroutine(ChangeScene());
        }
    }

    void SetStartedSinging()
    {
        startedSinging = true;
    }

    IEnumerator TwistItAllAround()
    {
        int RandomInt = Random.Range(0, 6);
        Vector3 dir = Vector3.zero;
        switch (RandomInt)
        {
            case 0:
                dir = Vector3.right;
                break;
            case 1:
                dir = Vector3.left;
                break;
            case 2:
                dir = Vector3.up;
                break;
            case 3:
                dir = Vector3.down;
                break;
            case 4:
                dir = Vector3.forward;
                break;
            case 5:
                dir = Vector3.back;
                break;
            default:
                dir = Vector3.right;
                break;
        }

        float elapsedTime = 0;
        float timer = 1;
        while (elapsedTime < timer)
        {
            timeText.transform.Rotate(dir * (elapsedTime / timer));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ChangeScene()
    {
        StartCoroutine(FindObjectOfType<Credits>().TurnScreenRed());
        yield return new WaitForSeconds(1);
        materialToChange.color = new Color(.3f, .3f, .3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
