using UnityEngine;

public class DragController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject[] draggableObjects;
    [SerializeField] private Transform targetPosition; 

    private GameObject _lastDragged;
    private Vector3 _worldPosition;
    private bool _isDragging = false;
    private Vector3 _initialPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectDraggableObject();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            CheckDropPosition();
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
                    _initialPosition = _lastDragged.transform.position; 
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
        _worldPosition.z = 0; 
        _lastDragged.transform.position = _worldPosition;
    }

    void CheckDropPosition()
    {
        if (_lastDragged != null)
        {
            Collider2D targetCollider = targetPosition.GetComponent<Collider2D>();
            Collider2D draggedCollider = _lastDragged.GetComponent<Collider2D>();

            if (targetCollider != null && draggedCollider != null)
            {
                if (draggedCollider.bounds.Intersects(targetCollider.bounds) && _lastDragged.name == "coin_0")
                {
                   
                    _lastDragged.transform.position = targetPosition.position;
                    Debug.Log("Coin berhasil ditempatkan!");
                }
                else
                {
                    
                    _lastDragged.transform.position = _initialPosition;
                    Debug.Log("Objek kembali ke posisi awal.");
                }
            }
        }
    }
}
