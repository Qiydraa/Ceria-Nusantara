using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject[] draggableObjects;

    private GameObject _lastDragged;
    private Vector3 _worldPosition;
    private bool _isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectDraggableObject();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (_isDragging && _lastDragged != null)
        {
            DragObject();
        }
    }

    void DetectDraggableObject()
    {
        Vector3 screenPosition = Input.mousePosition;
        _worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        foreach (var draggable in draggableObjects)
        {
            if (draggable != null)
            {
                Collider2D collider = draggable.GetComponent<Collider2D>();
                if (collider != null && collider.OverlapPoint(_worldPosition))
                {
                    _lastDragged = draggable;
                    _isDragging = true;
                    Debug.Log($"Started dragging: {_lastDragged.name}");
                    break;
                }
            }
        }
    }

    void DragObject()
    {
        Vector3 screenPosition = Input.mousePosition;
        _worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        _worldPosition.z = 0; // Ensure object stays in 2D plane
        _lastDragged.transform.position = _worldPosition;
    }
}
