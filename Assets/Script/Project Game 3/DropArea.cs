using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    [SerializeField] private string correctItemID;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        if (eventData.pointerDrag.TryGetComponent(out DragItem draggedItem))
        {
            if (draggedItem.ItemID == correctItemID)
            {
                Debug.Log("✅ Benar!");
                draggedItem.transform.SetParent(transform);
                draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                Debug.Log("❌ Salah!");
                draggedItem.ResetPosition();
            }
        }
    }
}
