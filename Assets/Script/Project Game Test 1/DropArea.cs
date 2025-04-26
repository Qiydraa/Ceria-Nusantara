using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableAnswer dragged = eventData.pointerDrag.GetComponent<DraggableAnswer>();

        if (dragged != null)
        {
            if (dragged.isCorrect)
            {
                dragged.LockToPosition(transform);
            }
            else
            {
                dragged.ResetPosition();
            }
        }
    }
}
