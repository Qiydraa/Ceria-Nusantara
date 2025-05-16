using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;

    [Tooltip("Posisi target benar, bisa dari GameObject kosong di canvas")]
    public RectTransform correctDropZone;

    [Tooltip("Jarak toleransi drop yang diterima")]
    public float dropTolerance = 50f;

    private Canvas canvas;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Object harus berada dalam Canvas!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;

        // Supaya saat drag objek muncul di depan semua
        transform.SetParent(canvas.transform);

        canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos);

        transform.position = canvas.transform.TransformPoint(pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        float distance = Vector3.Distance(transform.position, correctDropZone.position);

        if (distance <= dropTolerance)
        {
            // Jika drop benar, menempel di posisi target benar
            transform.position = correctDropZone.position;
            transform.SetParent(correctDropZone);
        }
        else
        {
            // Jika drop salah, kembali ke posisi awal
            transform.position = startPosition;
            transform.SetParent(startParent);
        }
    }
}
