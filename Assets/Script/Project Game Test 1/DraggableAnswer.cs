using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableAnswer : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    private Vector2 originalPosition;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public bool isCorrect = false;
    private bool isLocked = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isLocked) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!isLocked && transform.parent == originalParent) {
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    public void LockToPosition(Transform newParent)
    {
        isLocked = true;
        transform.SetParent(newParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }
}
