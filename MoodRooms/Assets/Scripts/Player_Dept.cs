using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum PlayerStatesGDP { NORMAL, CAN_TELEPORT_TO_OFFICE, CAN_PULL_MIRROR }

public class Player_Dept : MonoBehaviour 
{
    [SerializeField] GameObject showerMirror, officeMirror, cellMirror, officeCellMirror, finalCellDoor, bars;
    [SerializeField] Transform showerMirrorFinalPos, officemirrorFinalPos;
    [SerializeField] Camera showerCam, officeCam;
    [SerializeField] Text instructions;
    [SerializeField] float mirrorSpeed = 2;

    Vector3 originalMirrorPos;
    RenderTexture showerRenderTexture, officeRenderTexture;

    PlayerStatesGDP currentState;
    GameObject mirrorBeingPulled;
    Vector3 objectBeingLookedAt;

    bool movedLastMirror;

    void Start()
    {
        showerRenderTexture = officeCam.targetTexture;
        officeRenderTexture = showerCam.targetTexture;
    }

    void Update()
    {
        if (currentState == PlayerStatesGDP.CAN_TELEPORT_TO_OFFICE &&
            Vector3.Angle(transform.forward, objectBeingLookedAt) < Camera.main.fieldOfView)
            transform.position += new Vector3(0, 0, 8.6f);

        if (currentState == PlayerStatesGDP.CAN_PULL_MIRROR && Input.GetButton("Jump"))
        {
            if (mirrorBeingPulled == showerMirror)
                mirrorBeingPulled.transform.position = Vector3.Lerp(mirrorBeingPulled.transform.position, showerMirrorFinalPos.position, mirrorSpeed * Time.deltaTime);

            if (mirrorBeingPulled == officeMirror)
            {
                mirrorBeingPulled.transform.position = Vector3.Lerp(mirrorBeingPulled.transform.position, officemirrorFinalPos.position, mirrorSpeed * Time.deltaTime);

                if (Vector3.Distance(officeMirror.transform.position, officemirrorFinalPos.position) < 0.025f)
                {
                    showerCam.targetTexture = showerRenderTexture;
                    officeCam.targetTexture = officeRenderTexture;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FirstMirror")
        {
            transform.position += new Vector3(0, 0, -10);
            transform.Rotate(new Vector3(0, 180, 0));
            bars.SetActive(true);
        }

        if (other.tag == "SecondMirror")
        {
            transform.position += new Vector3(0, 0, -8.6f);
            officeCellMirror.tag = "SecondMirrorInactive";
        }

        if (other.tag == "CellMirror")
        {
            transform.position += new Vector3(0, 0, 8.6f);
            officeCellMirror.tag = "SecondMirrorInactive";
        }

        if (other.tag == "FirstTeleporter")
        {
            transform.position += (transform.forward / 2);
            transform.Rotate(new Vector3(0, 180, 0));
        }

        if (other.tag == "SecondTeleporter")
        {
            objectBeingLookedAt = other.transform.position;
            currentState = PlayerStatesGDP.CAN_TELEPORT_TO_OFFICE;
        }

        if (other.tag == "PullableMirror")
        {
            instructions.text = "Hold space to move mirrors";
            mirrorBeingPulled = other.gameObject;

            if (other.gameObject == showerMirror)
            {
                officeMirror.tag = "PullableMirror";
                cellMirror.tag = "CellMirror";
            }
            else if (other.gameObject == officeMirror)
            {
                cellMirror.tag = "Untagged";
                officeCellMirror.tag = "SecondMirror";
            }
            currentState = PlayerStatesGDP.CAN_PULL_MIRROR;
        }

        if (other.tag == "CellTrigger" && movedLastMirror && Vector3.Angle(transform.forward, showerMirror.transform.position) < Camera.main.fieldOfView)
        {
            finalCellDoor.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == officeMirror && other.tag == "PullableMirror") movedLastMirror = true;
        else if (!movedLastMirror) currentState = PlayerStatesGDP.NORMAL;

        instructions.text = "";
    }
}
