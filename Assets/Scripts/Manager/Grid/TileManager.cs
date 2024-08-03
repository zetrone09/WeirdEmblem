using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Color baseColor = Color.white;
    public Color offsetColor = Color.gray;
    public Color highlightColor = Color.green;
    public Renderer tileRenderer;

    public delegate void TileClicked(Vector3 position);
    public event TileClicked OnTileClicked;
    public void Init(bool isOffset)
    {
        tileRenderer.material.color = isOffset ? offsetColor : baseColor;
    }
    private void OnMouseDown()
    {
        OnTileClicked?.Invoke(transform.position);
    }
    private void Awake()
    {
        if (tileRenderer == null)
        {
            tileRenderer = GetComponent<Renderer>();
        }
    }
    public void SetColor(Color color)
    {
        if (tileRenderer != null)
        {
            tileRenderer.material.color = color;
        }
    }
}
