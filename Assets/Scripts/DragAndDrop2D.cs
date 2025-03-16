using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop2D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        void SnapToGrid()
        {
            float gridSize = 1.0f; // Change if needed
            Vector3 snappedPosition = new Vector3(
                Mathf.Round(transform.position.x / gridSize) * gridSize,
                Mathf.Round(transform.position.y / gridSize) * gridSize,
                0f
            );

            // Ensure block stays within grid bounds
            if (IsWithinGrid(snappedPosition))
            {
                transform.position = snappedPosition;
            }
            else
            {
                Debug.Log("Invalid Placement!"); // Prevents placing outside the table
            }
        }

        // Function to check if block is within the table grid
        bool IsWithinGrid(Vector3 position)
        {
            float minX = -5f, maxX = 5f; // Set grid boundaries
            float minY = -5f, maxY = 5f;

            return (position.x >= minX && position.x <= maxX &&
                    position.y >= minY && position.y <= maxY);
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        SnapToGrid();  // Adjusts position to nearest grid cell
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0f; // Ensure it's at the correct 2D depth
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    void SnapToGrid()
    {
        float gridSize = 1.0f; // Adjust based on your grid size
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            Mathf.Round(transform.position.y / gridSize) * gridSize,
            0f
        );
        transform.position = snappedPosition;
    }
}
