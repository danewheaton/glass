using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class HoverCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Texture2D pointer;

    public void OnPointerEnter(PointerEventData ped)
    {
        Cursor.SetCursor(pointer, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void OnPointerExit(PointerEventData ped)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
