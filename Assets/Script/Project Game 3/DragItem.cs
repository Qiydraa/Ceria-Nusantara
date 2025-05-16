using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Drag Settings")]
    [SerializeField] private string itemID;
    public string ItemID => itemID;

    [SerializeField] private Canvas parentCanvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (parentCanvas == null)
        {
            Debug.LogWarning($"[DragItem] Canvas belum di-assign di '{gameObject.name}'!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (parentCanvas == null) return;

        originalParent = transform.parent;
        originalPosition = rectTransform.anchoredPosition;

        canvasGroup.blocksRaycasts = false;

        transform.SetParent(parentCanvas.transform);
        transform.SetAsLastSibling(); // Supaya tampil di atas semua UI
    }

    public void OnDrag(PointerEventData eventData)
    {
        float scaleFactor = parentCanvas != null ? parentCanvas.scaleFactor : 1f;
        rectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Jika tidak di-drop ke area yang menerima
        if (parentCanvas != null && transform.parent == parentCanvas.transform)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = originalPosition;
    }
}
