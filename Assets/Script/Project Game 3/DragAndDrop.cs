using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentToReturnTo = null;

    public Transform correctDropZone;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root); // Agar tidak dipotong canvas mask
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        float distance = Vector3.Distance(transform.position, correctDropZone.position);

        if (distance < 50f) // Sesuaikan jarak toleransi
        {
            transform.position = correctDropZone.position;
        }
        else
        {
            transform.position = startPosition;
        }

        transform.SetParent(parentToReturnTo);
    }
}
