using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentTransform;
    private bool isPlaced = false;
    private GridManager gridManager;
    private GridCell closestCell;

    private void Start()
    {
        gridManager = GridManager.Instance;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        startPosition = transform.position;
        parentTransform = transform.parent;
        transform.SetParent(parentTransform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        closestCell = FindClosestGridCell();
        if (closestCell != null && gridManager.CanPlaceBlock(closestCell.gridPos))
        {
            transform.position = closestCell.transform.position;
        }
        else
        {
            transform.position = startPosition;
        }
    }

    private GridCell FindClosestGridCell()
    {
        GridCell[] cells = FindObjectsOfType<GridCell>();
        GridCell bestCell = null;
        float minDistance = float.MaxValue;

        foreach (var cell in cells)
        {
            float dist = Vector2.Distance(transform.position, cell.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                bestCell = cell;
            }
        }

        return bestCell;
    }

    public void ConfirmPlacement()
    {
        if (closestCell != null && gridManager.CanPlaceBlock(closestCell.gridPos))
        {
            gridManager.PlaceBlock(closestCell.gridPos);
            isPlaced = true;
        }
    }
}


