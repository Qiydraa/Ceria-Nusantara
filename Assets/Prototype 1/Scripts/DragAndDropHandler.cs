using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [SerializeField] private Button targetButton; // Button tujuan (contoh: Button Bali)
    [SerializeField] private Text resultText; // Text untuk menampilkan hasil

    private Vector3 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Buat gambar menjadi transparan saat di-drag
        canvasGroup.blocksRaycasts = false; // Hindari menghalangi raycast
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Gerakkan objek mengikuti kursor
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Cek apakah objek dilepaskan di atas Button target
        if (RectTransformUtility.RectangleContainsScreenPoint(
            targetButton.GetComponent<RectTransform>(),
            Input.mousePosition,
            canvas.worldCamera))
        {
            resultText.text = "Benar!";
        }
        else
        {
            resultText.text = "Salah!";
            rectTransform.position = originalPosition; // Kembalikan ke posisi awal
        }
    }
}
