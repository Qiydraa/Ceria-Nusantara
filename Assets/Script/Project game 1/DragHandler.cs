using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Transform startParent;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        // Pastikan ada CanvasGroup
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false; // Supaya bisa dilepas di dropzone
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        itemBeingDragged = null;

        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = true; // Aktifkan raycast lagi saat selesai drag
    }
}
