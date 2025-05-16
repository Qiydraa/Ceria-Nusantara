using UnityEngine;

public class PopupText : MonoBehaviour
{
    public float delayBeforeShow = 1f;  // waktu tunggu sebelum muncul
    public float showDuration = 3f;     // durasi tampil sebelum menghilang

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Awalnya disembunyikan
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    void Start()
    {
        StartCoroutine(ShowAndHidePopup());
    }

    System.Collections.IEnumerator ShowAndHidePopup()
    {
        // Tunggu sebelum menampilkan
        yield return new WaitForSeconds(delayBeforeShow);

        // Munculkan
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        // Tunggu sebelum menghilang
        yield return new WaitForSeconds(showDuration);

        // Hilangkan
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
