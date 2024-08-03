using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    [SerializeField]private GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        HighlightWalkableTiles();
    }
    private void Update()
    {
        if (isMoving)
        {
            MoveCharacter();
        }
        else
        {
            HighlightWalkableTiles();
        }
    }
    private void MoveCharacter()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if(Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
    private void HighlightWalkableTiles()
    {
        if (gridManager != null)
        {
            gridManager.HighlightWalkableTiles(transform.position, 3, true);
        }
    }
    public void SetTargetPosition(Vector3 position)
    {
        if (gridManager.GetWalkablePositions(transform.position, 3+1, false).Contains(position))
        {
            targetPosition = position;
            isMoving = true;
        }
        else if (gridManager.GetWalkablePositions(transform.position, 2+1, true).Contains(position))
        {
            targetPosition = position;
            isMoving = true;
        }
        else
        {
            Debug.LogWarning("Position is not walkable.");
        }
    }
}
