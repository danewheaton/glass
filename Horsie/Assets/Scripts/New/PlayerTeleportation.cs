using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public enum PlayerStates
{
    NORMAL,
    IN_NEW_REFECTORY_BUT_THE_PLAYER_DOESNT_KNOW_IT_YET,
    SHOULD_RETURN_TO_ORIGINAL_REFECTORY,
    IN_NEW_REFECTORY,
    LOOKING_AT_MIRROR,
    IN_GLASS2_TRIGGER,
    BEFORE_CATACOMBS
}

// LAYERS: Glass1, Door01, Door01Blocker, Door02, Door02Blocker, OldCourtyard, NewChurch, Portal01,
// MirrorDoor1, MirrorDoor1Blocker, MirrorDoor2Blocker, Glass2, NotEdgy

public class PlayerTeleportation : MonoBehaviour
{
    public GameObject party, nineFortyOne, clock, courtyardPortal, hiddenHallwayPortal, triggerAfterHiddenHallwayPortal, fallingportal, outerShard12, redFrame, directionalLight, startingDoorTrigger,
        startingDoorTriggerClockwise, glass0, glass0Copy, startingDoor, startingDoorBlocker, hallwayTrigger, hallwayWall01,
        hallwayWall02, teleporterTrigger01, triggerAfterTeleporter01, wallBlockingWay, teleporterTrigger02Right, teleporterTrigger02Left, narthexDoor,
        narthexDoorTrigger, narthexDoorBlocker, glass1Activator, glass1perspectivePuzzle, glass1gameObject, invisibleDoor01, invisibleDoor01Blocker,
        invisibleDoor02, invisibleDoor02Blocker, pews, altar, altarTeleporter, newChurch, oldCourtyard, door03, door03Trigger, door03Blocker,
        mirror, fakeMirror, mirrorDoor1, mirrorDoor1Trigger, mirrorDoor1Blocker, mirrorDoor2Blocker,
        mirrorDoor2Trigger, mirror2Trigger, glass2, glass2Trigger, returnToOriginalRefectoryTrigger, newRefectoryTrigger,
        doorToCatacombs, doorToCatacombsTrigger, doorAtBottomOfStairwell, doorAtBottomOfStairwellTrigger,
        doorAtBottomOfStairwellBlocker, catacombsUnlit, catacombsLit, endTrigger, staticAssets, dynamicAssets, observatory1, observatory2;
    public GameObject[] upperHallway, glassPortals, scrawlings, disappearingPassage, reappearingNook, observatoryMirrors;
    public Transform cam, refectoryWeenie, middle, startingDoorTransform, teleporter02Transform, glass1Transform, portal01Transform, mirror01Transform, glassShardTransform, fallingPortalCamTransform;
    public Transform[] playerStarts;
    public Rigidbody[] glassRigidBodies, moreGlassRbs;
    public Collider[] stairColliders;
    public Material beigeMaterial, whiteMaterial, oldCourtyardMaterial;
    public AudioSource cymbal;
    public AudioClip victory, crashCymbal;
    public Credits creditsPanel;

    PlayerStates currentState;
    Vector3 originalScale, targetScale = new Vector3(.2f, .2f, .2f), shardTarget, shardOriginal;
    DynamicMusic music;
    EdgeDetection ed;

    AudioClip originalChime;

    float timeCounter;
    int laps, portalsBroken, collectibles;
    bool passedThrough, hittingForeground, hittingBackground, wentAroundOnce;

    void OnDrawGizmos()
    {
        Debug.DrawRay(Camera.main.transform.position, transform.forward);
    }

    void Start()
    {
        originalScale = transform.localScale;
        if (outerShard12 != null) shardTarget = outerShard12.transform.position;
        if (outerShard12 != null) shardOriginal = outerShard12.transform.position;
        music = FindObjectOfType<DynamicMusic>();
        ed = GetComponentInChildren<EdgeDetection>();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        Mathf.Floor(timeCounter);

        #region new shit

        if (outerShard12 != null) outerShard12.transform.position = Vector3.Lerp(outerShard12.transform.position, shardTarget, Time.deltaTime);

        #endregion


        if (altar != null && Vector3.Distance(transform.position, altar.transform.position) < 1.5f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 2 * Time.deltaTime);
            GetComponent<vp_FPController>().MotorAcceleration = .06f;
        }
        else if (altar != null && Vector3.Distance(transform.position, altar.transform.position) < 5)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, 2 * Time.deltaTime);
            GetComponent<vp_FPController>().MotorAcceleration = .12f;
        }

        switch (currentState)
        {
            case PlayerStates.NORMAL:
                if (mirrorDoor2Blocker != null && !mirrorDoor2Blocker.activeInHierarchy) mirrorDoor2Blocker.SetActive(true);
                break;
            case PlayerStates.IN_NEW_REFECTORY_BUT_THE_PLAYER_DOESNT_KNOW_IT_YET:
                Vector3 fakeMirrorDirection = fakeMirror.transform.position - transform.position;

                if (Vector3.Angle(fakeMirrorDirection, transform.forward) > 60)
                {
                    mirrorDoor1Blocker.SetActive(true);
                }
                break;
            case PlayerStates.SHOULD_RETURN_TO_ORIGINAL_REFECTORY:
                Vector3 mirrorDirection = mirror.transform.position - transform.position;

                if (Vector3.Angle(mirrorDirection, transform.forward) > 60)
                {
                    transform.position += new Vector3(-20, 0, -20);
                    mirror.SetActive(true);
                    fakeMirror.SetActive(false);
                    currentState = PlayerStates.NORMAL;
                }
                break;
            case PlayerStates.IN_NEW_REFECTORY:
                Vector3 weenieDirection = new Vector3(14, refectoryWeenie.position.y, refectoryWeenie.position.z) - transform.position;

                if (Vector3.Angle(weenieDirection, transform.forward) < 45)
                {
                    // TODO: weed out the bug here
                    passedThrough = true;
                    foreach (GameObject g in disappearingPassage) g.SetActive(false);
                    foreach (GameObject g in reappearingNook) g.SetActive(true);
                    returnToOriginalRefectoryTrigger.SetActive(false);
                    mirror.SetActive(true);
                    fakeMirror.SetActive(false);
                    mirror.transform.position = mirror01Transform.position;
                    fakeMirror.SetActive(false);
                    mirrorDoor1Blocker.SetActive(false);
                    currentState = PlayerStates.NORMAL;
                }
                if (!mirrorDoor2Blocker.activeInHierarchy) mirrorDoor2Blocker.SetActive(true);
                break;
            case PlayerStates.LOOKING_AT_MIRROR:
                Vector3 mirror2Direction = mirror.transform.position - transform.position;

                if (Vector3.Angle(mirror2Direction, transform.forward) >= 45)
                {
                    mirrorDoor2Blocker.SetActive(true);
                    mirror.GetComponentInChildren<Mirror>().PlayerIsOnFarSideOfTableOrLookingAway();
                    currentState = PlayerStates.NORMAL;
                }
                break;
        }

        if (mirrorDoor2Blocker != null && mirrorDoor2Blocker.activeInHierarchy) glass2.GetComponent<Renderer>().enabled = false;
        else if (glass2 != null) glass2.GetComponent<Renderer>().enabled = true;

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.K)) transform.position = playerStarts[1].position;
            if (Input.GetKeyDown(KeyCode.L))
            {
                transform.position = playerStarts[2].position;

                //oldCourtyard.SetActive(false);
                Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                    (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                    (1 << LayerMask.NameToLayer("MirrorDoor1Blocker")) | (1 << LayerMask.NameToLayer("MirrorDoor2Blocker")) |
                    (1 << LayerMask.NameToLayer("Glass2")));

                Collider[] collidersAgain = newChurch.GetComponentsInChildren<Collider>();
                foreach (Collider c in collidersAgain) c.enabled = true;
                //Renderer[] churchRenderers = newChurch.GetComponentsInChildren<Renderer>();
                //foreach (Renderer r in churchRenderers) r.material = beigeMaterial;
                //Renderer[] pewRenderers = pews.GetComponentsInChildren<Renderer>();
                //foreach (Renderer r in pewRenderers) r.material = whiteMaterial;

                door03.SetActive(false);
                door03Blocker.SetActive(true);
                mirror.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.K)) transform.position = playerStarts[1].position;
            if (Input.GetKeyDown(KeyCode.L)) transform.position = playerStarts[2].position;
            if (Input.GetKeyDown(KeyCode.Semicolon)) transform.position = playerStarts[3].position;
        }
    }




    void OnTriggerEnter(Collider other)
    {
        #region new shit

        if (other.tag == "Oculus")
        {
            if (collectibles < glassRigidBodies.Length) glassRigidBodies[collectibles].gameObject.SetActive(true);
            
            collectibles++;
            other.gameObject.SetActive(false);

            if (glassRigidBodies[glassRigidBodies.Length - 1].gameObject.activeInHierarchy)
            {
                Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                        (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("UI")) |
                        (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                        (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("OldCourtyard")) |
                        (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("ObservatoryMirror")) | (1 << LayerMask.NameToLayer("BigShard")));

                //redFrame.GetComponent<Collider>().enabled = true;
                //foreach (GameObject go in observatoryMirrors) go.SetActive(false);

                originalChime = creditsPanel.soundFeed.warpSound;
                creditsPanel.soundFeed.warpSound = victory;
                StartCoroutine(creditsPanel.FlashWhite());
                GetComponent<vp_FPController>().SetPosition(middle.position);
                GetComponentInChildren<vp_FPCamera>().SetRotation(new Vector2(-44.585f, 135), false);
                cam.transform.rotation = new Quaternion(-44.585f, 0, 0, 0);
                GameObject[] remainingShards = GameObject.FindGameObjectsWithTag("Oculus");
                foreach (GameObject g in remainingShards) g.SetActive(false);
            }
            else
            {
                StartCoroutine(creditsPanel.FlashRewardText(collectibles));
                timeCounter = 0;
            }
            
        }
        if (other.tag == "RedPillar")
        {
            creditsPanel.soundFeed.warpSound = crashCymbal;
            StartCoroutine(ChangeScene(Color.red));
        }

        else if (other.gameObject == redFrame)
        {
            foreach (Rigidbody r in glassRigidBodies)
            {
                r.transform.parent = null;
                r.isKinematic = false;
            }

            foreach (Rigidbody r in moreGlassRbs)
            {
                r.transform.parent = null;
                r.isKinematic = false;
            }

            outerShard12.transform.parent = null;
            outerShard12.transform.eulerAngles -= new Vector3(20, 0, 0);
            shardTarget -= new Vector3(0, 11, 0);
            cymbal.Play();
        }

        else if (other.gameObject == outerShard12)
        {
            Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                            (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Water")) |
                            (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                            (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("OldCourtyard")) |
                            (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("BigShard")));

            foreach (Collider c in stairColliders) c.enabled = true;

            creditsPanel.soundFeed.warpSound = originalChime;
            StartCoroutine(creditsPanel.FlashWhite());
            outerShard12.SetActive(false);
            redFrame.GetComponent<Collider>().enabled = false;
        }

        else if (other.gameObject == fallingportal)
        {
            StartCoroutine(creditsPanel.FlashRandomColor());
            transform.position = fallingPortalCamTransform.position;

            Renderer[] renderers = newChurch.GetComponentsInChildren<Renderer>();
            
            observatory2.SetActive(false);
            courtyardPortal.SetActive(false);

            foreach (Renderer r in renderers) r.gameObject.layer = LayerMask.NameToLayer("NotEdgy");
            Collider[] colliders = newChurch.GetComponentsInChildren<Collider>();
            foreach (Collider c in colliders) c.enabled = false;

            glass1gameObject.SetActive(false);
            invisibleDoor02.SetActive(false);
            invisibleDoor02Blocker.SetActive(true);

            foreach (GameObject g in upperHallway)
            {
                g.GetComponent<Collider>().enabled = true;
            }
        }

        else if (other.gameObject == hiddenHallwayPortal)
        {
            StartCoroutine(creditsPanel.FlashRandomColor());

            foreach (GameObject g in upperHallway)
            {
                g.layer = LayerMask.NameToLayer("Default");
                g.GetComponent<Renderer>().material = beigeMaterial;
            }
        }

        else if (other.gameObject == triggerAfterHiddenHallwayPortal)
        {
            oldCourtyard.SetActive(false);

            Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("UI")) |
                (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                (1 << LayerMask.NameToLayer("MirrorDoor1Blocker")) | (1 << LayerMask.NameToLayer("MirrorDoor2Blocker")) |
                (1 << LayerMask.NameToLayer("Glass2")) | (1 << LayerMask.NameToLayer("ObservatoryMirror")) | (1 << LayerMask.NameToLayer("BigShard")));

            Collider[] collidersAgain = newChurch.GetComponentsInChildren<Collider>();
            foreach (Collider c in collidersAgain) c.enabled = true;
            Renderer[] renderers = newChurch.GetComponentsInChildren<Renderer>();
            //foreach (Renderer r in renderers) r.material = oldCourtyardMaterial;
            foreach (Renderer r in renderers) r.gameObject.layer = LayerMask.NameToLayer("Default");
            Renderer[] churchRenderers = newChurch.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in churchRenderers) r.material = beigeMaterial;
            
            Renderer[] pewRenderers = pews.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in pewRenderers) r.material = whiteMaterial;
        }

        #endregion

        else if (other.gameObject == startingDoor)
        {
            StartCoroutine(music.ToggleTrack(true, "kickDrum"));
        }
        
        else if (other.gameObject == hallwayTrigger)
        {
            hallwayWall01.SetActive(false);
            hallwayWall02.SetActive(true);
            StartCoroutine(music.ToggleTrack(true, "snare"));
            ed.enabled = true;
        }

        else if (other.gameObject == teleporterTrigger01)
        {
            transform.position -= new Vector3(10, 0, 10);
        }

        else if (other.gameObject == triggerAfterTeleporter01)
        {
            if (wentAroundOnce)
            {
                wallBlockingWay.SetActive(false);
                StartCoroutine(music.ToggleTrack(true, "hiHat"));
            }
            wentAroundOnce = true;
        }

        else if (other.gameObject == teleporterTrigger02Right || other.gameObject == teleporterTrigger02Left)
        {
            Vector3 targetDirection = teleporter02Transform.position - transform.position;

            if (Vector3.Angle(targetDirection, transform.forward) < 180)
            {
                transform.position += new Vector3(15, 5, -50);
            }
        }

        else if (other.gameObject == narthexDoorTrigger)
        {
            narthexDoor.SetActive(false);
            narthexDoorBlocker.SetActive(true);
            StartCoroutine(music.ToggleTrack(true, "guitar"));
            StartCoroutine(music.ToggleTrack(true, "cymbals"));
        }

        else if (other.gameObject == altarTeleporter)
        {
            StartCoroutine(ChangeScene(Color.white));
        }

        else if (other.gameObject == glass1Activator)
        {
            glass1perspectivePuzzle.SetActive(true);
            glass1Activator.SetActive(false);
        }

        else if (other.gameObject == glass1gameObject)
        {
            Vector3 targetDirection = invisibleDoor01.transform.position - transform.position;

            if (Vector3.Angle(targetDirection, transform.forward) < 90)
            {
                StartCoroutine(creditsPanel.FlashRandomColor());

                invisibleDoor01Blocker.SetActive(false);
                invisibleDoor01.GetComponent<Renderer>().material = beigeMaterial;
                Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                    (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("OldCourtyard")) |
                    (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("BigShard")));

                Renderer[] renderers = newChurch.GetComponentsInChildren<Renderer>();

                foreach (Renderer r in renderers) r.gameObject.layer = LayerMask.NameToLayer("NotEdgy");
                Collider[] colliders = newChurch.GetComponentsInChildren<Collider>();
                foreach (Collider c in colliders) c.enabled = false;

                glass1gameObject.SetActive(false);
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Door02"))
        {
            invisibleDoor02.SetActive(false);
            invisibleDoor02Blocker.SetActive(true);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Portal01"))
        {
            Vector3 targetDirection = portal01Transform.position - transform.position;

            if (Vector3.Angle(targetDirection, transform.forward) > 90)
            {
                StartCoroutine(creditsPanel.FlashRandomColor());
                oldCourtyard.SetActive(false);
                Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                    (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                    (1 << LayerMask.NameToLayer("MirrorDoor1Blocker")) | (1 << LayerMask.NameToLayer("MirrorDoor2Blocker")) |
                    (1 << LayerMask.NameToLayer("Glass2")) | (1 << LayerMask.NameToLayer("ObservatoryMirror")) | (1 << LayerMask.NameToLayer("BigShard")));

                Collider[] collidersAgain = newChurch.GetComponentsInChildren<Collider>();
                foreach (Collider c in collidersAgain) c.enabled = true;
                Renderer[] renderers = newChurch.GetComponentsInChildren<Renderer>();
                //foreach (Renderer r in renderers) r.material = oldCourtyardMaterial;
                foreach (Renderer r in renderers) r.gameObject.layer = LayerMask.NameToLayer("Default");
                Renderer[] churchRenderers = newChurch.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in churchRenderers) r.material = beigeMaterial;

                Renderer[] pewRenderers = pews.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in pewRenderers) r.material = whiteMaterial;
            }
        }
        else if (other.gameObject == door03Trigger)
        {
            door03.SetActive(false);
            door03Blocker.SetActive(true);
            mirror.SetActive(true);
        }

        else if (other.gameObject == mirrorDoor1Trigger)
        {
            Vector3 targetDirection = mirror.transform.position - transform.position;

            if (Vector3.Angle(targetDirection, transform.forward) < 45)
            {
                transform.position += new Vector3(20, 0, 20);
                fakeMirror.SetActive(true);
                mirror.SetActive(false);
                currentState = PlayerStates.IN_NEW_REFECTORY_BUT_THE_PLAYER_DOESNT_KNOW_IT_YET;
            }
        }

        else if (other.gameObject == returnToOriginalRefectoryTrigger && !passedThrough)
        {
            mirrorDoor1Blocker.SetActive(false);
            currentState = PlayerStates.SHOULD_RETURN_TO_ORIGINAL_REFECTORY;
        }

        else if (other.gameObject == newRefectoryTrigger)
        {
            currentState = PlayerStates.IN_NEW_REFECTORY;
        }

        else if (other.gameObject == mirror2Trigger)
        {
            mirror.GetComponentInChildren<Mirror>().PlayerIsCloseAndLookingAtMirror();
        }

        else if (other.gameObject == mirrorDoor2Trigger && currentState == PlayerStates.LOOKING_AT_MIRROR)
        {
            mirrorDoor2Blocker.SetActive(false);
            nineFortyOne.SetActive(false);
        }

        else if (other.gameObject == glass2Trigger)
        {
            glass2.GetComponent<Collider>().enabled = true;
            currentState = PlayerStates.IN_GLASS2_TRIGGER;
        }

        else if (other.gameObject == glass2)
        {
            if (glass2.GetComponent<Collider>().enabled == true)
                StartCoroutine(creditsPanel.FlashRandomColor());
            
            clock.SetActive(true);
            transform.position += new Vector3(0, 0, 50);
            mirror.transform.position += new Vector3(0, 0, 50);
            //doorToCatacombs.SetActive(true);
            fakeMirror.SetActive(false);

            Camera.main.cullingMask = ((1 << LayerMask.NameToLayer("Default")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("UI")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) | (1 << LayerMask.NameToLayer("Door02")) |
                    (1 << LayerMask.NameToLayer("Door02Blocker")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                    (1 << LayerMask.NameToLayer("MirrorDoor1Blocker")) | (1 << LayerMask.NameToLayer("MirrorDoor2Blocker")) |
                    (1 << LayerMask.NameToLayer("Glass2")) | (1 << LayerMask.NameToLayer("ObservatoryMirror")) | (1 << LayerMask.NameToLayer("BigShard")));

            GetComponentInChildren<EdgeDetection>().enabled = true;
            GetComponentInChildren<NoiseAndGrain>().enabled = true;

            currentState = PlayerStates.BEFORE_CATACOMBS;
        }

        else if (other.gameObject == doorToCatacombsTrigger)
        {
            StartCoroutine(creditsPanel.FlashRandomColor());
            transform.position += new Vector3(0, 0, 50);
        }

        else if (other.gameObject == doorAtBottomOfStairwellTrigger)
        {
            doorAtBottomOfStairwellBlocker.SetActive(true);
            doorAtBottomOfStairwell.SetActive(false);
            mirror.SetActive(false);
            staticAssets.SetActive(false);
            dynamicAssets.SetActive(false);
            observatory1.SetActive(false);
            observatory2.SetActive(false);
            doorToCatacombs.SetActive(false);
        }

        else if (other.gameObject == endTrigger)
        {
            Credits.Won = true;

            catacombsUnlit.SetActive(false);
            catacombsLit.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (mirror2Trigger != null && other.gameObject == mirror2Trigger)
        {
            Vector3 targetDirection = mirror.transform.position - transform.position;

            if (Vector3.Angle(targetDirection, transform.forward) < 45)
                currentState = PlayerStates.LOOKING_AT_MIRROR;
            else currentState = PlayerStates.NORMAL;
        }

        //if (other.gameObject == playerStarts[1].gameObject)
        //{
        //    GetComponent<vp_FPController>().MotorAcceleration = .03f;

        //    RaycastHit[] hits;
        //    Ray ray = new Ray(Camera.main.transform.position, transform.forward);
        //    hits = Physics.RaycastAll(ray, 20);

        //    foreach (RaycastHit hit in hits)
        //    {
        //        if (hit.transform.gameObject.tag == "Foreground")
        //            hittingForeground = true;

        //        if (hit.transform.gameObject.tag == "Background")
        //            hittingBackground = true;

        //        if (hittingBackground && hittingForeground)
        //        {
        //            Invoke("ActivateGlass1", 1);
        //        }
        //        if (glass1perspectivePuzzle.activeInHierarchy)
        //            GetComponent<vp_FPController>().MotorAcceleration = .03f;
        //        else GetComponent<vp_FPController>().MotorAcceleration = .12f;
        //    }
        //}
    }

    void OnTriggerExit (Collider other)
    {
        //if (other.gameObject == playerStarts[1].gameObject)
        //{
        //    GetComponent<vp_FPController>().MotorAcceleration = .12f;

        //    hittingBackground = false;
        //    hittingForeground = false;
        //}

        if (other.gameObject == mirror2Trigger)
        {
            if (currentState != PlayerStates.LOOKING_AT_MIRROR && currentState != PlayerStates.IN_GLASS2_TRIGGER)
            {
                mirror.GetComponentInChildren<Mirror>().PlayerIsOnFarSideOfTableOrLookingAway();
                mirrorDoor2Blocker.SetActive(true);
            }
        }
    }

    IEnumerator LerpToPosition (Vector3 newPos)
    {
        Vector3 oldPos = transform.position;

        float timer = .1f;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            transform.position = Vector3.Lerp(oldPos, newPos, elapsedTime / timer);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ChangeScene(Color colorToBecome)
    {
        if (colorToBecome == Color.white)
            StartCoroutine(FindObjectOfType<Credits>().TurnScreenWhite());
        else if (colorToBecome == Color.red)
            StartCoroutine(FindObjectOfType<Credits>().TurnScreenRed());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ActivateGlass1()
    {
        if (hittingBackground && hittingForeground)
        {
            hittingBackground = false;
            hittingForeground = false;

            StartCoroutine(creditsPanel.FlashRandomColor());
            glass1perspectivePuzzle.SetActive(false);
            glass1gameObject.SetActive(true);
        }
    }
}
