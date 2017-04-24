using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour
{
    public Renderer mirrorDoor2Blocker;

    [SerializeField]
    SpriteRenderer mirrorSprite;

    Camera cam;
    Color originalColorTop;
    Color originalColorBottom;
    Color originalSpriteColor;

    void Start()
    {
        cam = GetComponent<Camera>();
        originalColorTop = mirrorDoor2Blocker.material.GetColor("_TopColor");
        originalColorBottom = mirrorDoor2Blocker.material.GetColor("_BottomColor");
        if (mirrorSprite != null)
        {
            originalSpriteColor = mirrorSprite.color;
            mirrorSprite.color = Color.clear;
        }

        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                    (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")) |
			(1 << LayerMask.NameToLayer("MirrorDoor2Blocker")));
    }

    public void PlayerIsOnFarSideOfTableOrLookingAway()
    {
        StopCoroutine(FadeMirrorImage());
        mirrorDoor2Blocker.material.SetColor("_TopColor", originalColorTop);
        mirrorDoor2Blocker.material.SetColor("_BottomColor", originalColorBottom);
        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
                    (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")) |
			(1 << LayerMask.NameToLayer("MirrorDoor2Blocker")));
    }

    public void PlayerIsCloseAndLookingAtMirror()
    {
        if (cam.cullingMask != ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
			(1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1"))))
        StartCoroutine(FlashMirrorSprite());
    }

    IEnumerator FlashMirrorSprite()
    {
        float elapsedTime = 0;
        float timer = .2f;
        while (elapsedTime < timer)
        {
            mirrorSprite.color = Color.Lerp(Color.clear, originalSpriteColor, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        mirrorSprite.color = originalSpriteColor;
        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
            (1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")));

        elapsedTime = 0;
        timer = 3;
        while (elapsedTime < timer)
        {
            mirrorSprite.color = Color.Lerp(originalSpriteColor, Color.clear, elapsedTime / timer);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        mirrorSprite.color = Color.clear;
    }

    IEnumerator FadeMirrorImage()
    {
        yield return new WaitForEndOfFrame();

        float timer = .5f;
        float elapsedTime = 0;
        while (elapsedTime < timer)
        {
            mirrorDoor2Blocker.material.SetColor("_TopColor", Color.Lerp(originalColorTop, Color.clear, elapsedTime / timer));
            mirrorDoor2Blocker.material.SetColor("_BottomColor", Color.Lerp(originalColorBottom, Color.clear, elapsedTime / timer));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        mirrorDoor2Blocker.material.SetColor("_TopColor", Color.clear);
        mirrorDoor2Blocker.material.SetColor("_BottomColor", Color.clear);

        cam.cullingMask = ((1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("TransparentFX")) |
                    (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Water")) |
                    (1 << LayerMask.NameToLayer("UI")) | (1 << LayerMask.NameToLayer("Glass1")) |
                    (1 << LayerMask.NameToLayer("Door01")) | (1 << LayerMask.NameToLayer("Door01Blocker")) |
                    (1 << LayerMask.NameToLayer("Door02")) | (1 << LayerMask.NameToLayer("Door02Blocker")) |
                    (1 << LayerMask.NameToLayer("OldCourtyard")) | (1 << LayerMask.NameToLayer("NewChurch")) |
			(1 << LayerMask.NameToLayer("Portal01")) | (1 << LayerMask.NameToLayer("MirrorDoor1")));
    }
}
