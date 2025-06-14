using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUI1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemRegion;
    public string targetSlot;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private Vector3 originalPosition;
    private Transform originalParent;

    public float snapDistance = 100f;

    [Header("SFX")]
    public AudioClip correctSFX;
    public AudioClip wrongSFX;
    private AudioSource audioSource;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.localPosition;
        originalParent = transform.parent;

        audioSource = Camera.main.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("Audio Source");
            audioObj.transform.SetParent(Camera.main.transform);
            audioSource = audioObj.AddComponent<AudioSource>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform); // agar item di atas UI lain
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DropZone1 nearestMatch = null;
        float nearestDistance = float.MaxValue;

        foreach (DropZone1 zone in DropZone1.AllZones)
        {
            RectTransform zoneRect = zone.GetComponent<RectTransform>();

            // GUNAKAN anchoredPosition untuk akurasi di UI
            float distance = Vector2.Distance(rectTransform.anchoredPosition, zoneRect.anchoredPosition);

            if (distance < snapDistance &&
                zone.region == itemRegion &&
                zone.slot == targetSlot)
            {
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestMatch = zone;
                }
            }
        }

        if (nearestMatch != null)
        {
            Debug.Log("✅ Berhasil ditempel ke zona: " + nearestMatch.name);
            AttachTo(nearestMatch.transform);

            if (correctSFX != null && audioSource != null)
                audioSource.PlayOneShot(correctSFX);

            FindObjectOfType<ContentManager>()?.CheckCompletion();
        }
        else
        {
            Debug.Log("❌ Tidak cocok atau terlalu jauh, kembali ke posisi awal.");
            ReturnToStart();

            if (wrongSFX != null && audioSource != null)
                audioSource.PlayOneShot(wrongSFX);
        }

        canvasGroup.blocksRaycasts = true;
    }

    public void AttachTo(Transform parent)
    {
        transform.SetParent(parent);
        rectTransform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
    }

    public void ReturnToStart()
    {
        transform.SetParent(originalParent);
        rectTransform.localPosition = originalPosition;
        gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
    }
}
