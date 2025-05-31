using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUI1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemRegion; // "Bali"
    public string targetSlot; // "Head"

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private Vector3 originalPosition;
    private Transform originalParent;

    public float snapDistance = 100f; // jarak maksimum untuk menempel

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

        // Inisialisasi AudioSource dari kamera utama
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
        transform.SetParent(canvas.transform); // agar di atas UI lain
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
        // Cari drop zone terdekat yang cocok
        DropZone1 nearestMatch = null;
        float nearestDistance = float.MaxValue;

        foreach (DropZone1 zone in DropZone1.AllZones)
        {
            RectTransform zoneRect = zone.GetComponent<RectTransform>();
            float distance = Vector2.Distance(rectTransform.position, zoneRect.position);

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

            // 🔊 Mainkan suara benar
            if (correctSFX != null && audioSource != null)
                audioSource.PlayOneShot(correctSFX);

            // Cek apakah semua item sudah cocok
            FindObjectOfType<ContentManager>()?.CheckCompletion();
        }
        else
        {
            Debug.Log("❌ Tidak cocok, kembali ke posisi awal.");
            ReturnToStart();

            // 🔊 Mainkan suara salah
            if (wrongSFX != null && audioSource != null)
                audioSource.PlayOneShot(wrongSFX);
        }

        // Pastikan raycast diaktifkan kembali
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
        gameObject.SetActive(true); // pastikan aktif
        canvasGroup.blocksRaycasts = true; // pastikan bisa di-drag lagi
    }
}
