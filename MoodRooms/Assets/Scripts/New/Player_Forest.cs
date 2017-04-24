using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player_Forest : MonoBehaviour
{
    [SerializeField]
    GameObject shard1, shard2, shard3, redFrame, outerShard12, fallingportal;

    [SerializeField]
    GameObject[] glassPortals, observatoryMirrors;

    [SerializeField]
    Transform fallingPortalCamTransform;

    [SerializeField]
    Rigidbody[] glassRigidBodies;

    [SerializeField]
    AudioSource cymbal;

    [SerializeField]
    Credits creditsPanel;

    Vector3 shardTarget, shardOriginal;

    int portalsBroken;
    bool passedThrough;

    private void Start()
    {
        shardTarget = outerShard12.transform.position;
        shardOriginal = outerShard12.transform.position;
    }

    private void Update()
    {
        outerShard12.transform.position = Vector3.Lerp(outerShard12.transform.position, shardTarget, Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject g in glassPortals)
        {
            if (other.gameObject == g)
            {
                StartCoroutine(creditsPanel.FlashRandomColor());
                g.SetActive(false);
                portalsBroken++;

                if (portalsBroken >= glassPortals.Length)
                {
                    Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                            (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("UI")) |
                            (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                            (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("OldCourtyard")) |
                            (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("ObservatoryMirror")) | (1 << LayerMask.NameToLayer("BigShard")));

                    redFrame.GetComponent<Collider>().enabled = true;
                    foreach (GameObject go in observatoryMirrors) go.SetActive(false);
                }
            }
        }

        if (other.gameObject == glassPortals[0])
        {
            shard1.SetActive(true);
        }
        else if (other.gameObject == glassPortals[1])
        {
            shard2.SetActive(true);
        }
        else if (other.gameObject == glassPortals[2])
        {
            shard3.SetActive(true);
        }

        else if (other.gameObject == redFrame)
        {
            foreach (Rigidbody r in glassRigidBodies)
            {
                r.transform.parent = null;
                r.isKinematic = false;
            }

            outerShard12.transform.parent = null;
            outerShard12.transform.eulerAngles -= new Vector3(20, 0, 0);
            shardTarget -= new Vector3(0, 5, 0);
            cymbal.Play();
            redFrame.GetComponent<Collider>().enabled = false;
        }

        else if (other.gameObject == outerShard12)
        {
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator ChangeScene()
    {
        StartCoroutine(FindObjectOfType<Credits>().FlashRed());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
