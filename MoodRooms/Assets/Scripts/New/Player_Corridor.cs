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

    NonUFPSPlayerController controller;
    float originalSpeed;
    int counter;
    int speedCharge;

    void Start()
    {
        materialToChange.color = new Color(.3f, .3f, .3f);
        controller = GetComponent<NonUFPSPlayerController>();
        originalSpeed = controller.movementSpeed;
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") <= 0) speedCharge = 0;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Mirror1" &&
            Vector3.Angle(other.transform.position - transform.position, transform.forward) <= Camera.main.fieldOfView)
        {
            transform.Rotate(Vector3.up * 180);
            transform.position += new Vector3(10, 0, 0);
            controller.movementSpeed = speedCurve.Evaluate(speedCharge);
            if (controller.movementSpeed >= 20) StartCoroutine(ChangeScene());
            counter++;
            speedCharge += 2;
            if (counter < rooms.Length) rooms[counter].SetActive(true);
            rooms[counter - 1].SetActive(false);

            Color newColor = new Color(materialToChange.color.r + .02f, materialToChange.color.g - .02f, materialToChange.color.b - .02f);
            materialToChange.color = newColor;

            if (counter > 7)
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
            if (counter > 20)
            {
                StartCoroutine(TwistItAllAround());
            }
            if (counter > 40)
            {
                timeText.GetComponent<TextMesh>().font = fonts[Random.Range(0, fonts.Length)];
            }
        }

        if (other.gameObject.tag == "Pen" || other.gameObject.tag == "Finish")
        {
            StartCoroutine(ChangeScene());
        }
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
        StartCoroutine(FindObjectOfType<Credits>().FlashRed());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
