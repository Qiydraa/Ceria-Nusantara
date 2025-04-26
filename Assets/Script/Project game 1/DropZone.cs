using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string jawabanBenar = "Ta'a";

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (dropped != null)
        {
            DragHandler dragHandler = dropped.GetComponent<DragHandler>();
            RectTransform droppedRect = dropped.GetComponent<RectTransform>();

            if (dropped.name.Contains(jawabanBenar))
            {
                // Set parent ke DropZone
                dropped.transform.SetParent(transform);

                // Pastikan ukurannya tetap normal
                droppedRect.localScale = Vector3.one;

                // Reset posisi agar berada di tengah DropZone
                droppedRect.anchoredPosition = Vector2.zero;

                // Pastikan tidak ada pergeseran Z
                Vector3 pos = droppedRect.localPosition;
                pos.z = 0;
                droppedRect.localPosition = pos;

                // Matikan drag agar tidak bisa digeser lagi
                if (dragHandler != null)
                    dragHandler.enabled = false;

                // Nonaktifkan interaksi agar tidak bisa di-drag lagi
                CanvasGroup cg = dropped.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    cg.blocksRaycasts = false;
                    cg.interactable = false;
                }

                Debug.Log("✅ Jawaban benar!");
            }
            else
            {
                // Jawaban salah, kembalikan ke posisi awal
                if (dragHandler != null)
                {
                    dropped.transform.SetParent(dragHandler.startParent);
                    dropped.transform.position = dragHandler.startPosition;
                }

                Debug.Log("❌ Jawaban salah!");
            }
        }
    }
}
