using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    public TileManager tilePrefab;
    public Transform cam;
    public CharacterManager character; 
    public List<Vector3> walkableTiles = new List<Vector3>();
    public List<TileManager> Tiles = new List<TileManager>();
    public Transform tileParent;

    private void Awake()
    {
        GenerateGrid();
    }
    private void GenerateGrid()
    {
        walkableTiles.Clear();
        Tiles.Clear();

        for (int x = 0; x < width; x++) 
        {
            for (int y = 0; y < height; y++) 
            { 
                var spawnedTile = Instantiate(tilePrefab,new Vector3(x,0,y),Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                var isOffset = (x + y) % 2 == 1;
                spawnedTile.Init(isOffset);

                walkableTiles.Add(spawnedTile.transform.position);
                Tiles.Add(spawnedTile);

                BoxCollider collider = spawnedTile.gameObject.AddComponent<BoxCollider>();
                collider.size = Vector3.one; // Set size to 1x1 unit, assuming the tile is 1x1 unit
                collider.center = Vector3.zero;

                var tileScript = spawnedTile.GetComponent<TileManager>();
                if (tileScript != null)
                {
                    tileScript.OnTileClicked += OnTileClicked;
                }
                else
                {
                    Debug.LogError("Tile component missing on spawned tile.");
                }
            }
        }
        
        
    }
    public void HighlightWalkableTiles(Vector3 characterPosition, int maxSteps, bool diagonal)
    {
        foreach (var tile in Tiles)
        {
            Vector3 tilePosition = tile.transform.position;
            if (IsTileWalkable(characterPosition, tilePosition, maxSteps, diagonal))
            {
                tile.SetColor(tile.highlightColor); // Highlight walkable tile
            }
            else
            {
                tile.SetColor(tile.baseColor); // Reset non-walkable tile
            }
        }
    }
    private bool IsTileWalkable(Vector3 startPosition, Vector3 tilePosition, int maxSteps, bool diagonal)
    {
        Vector3 direction = tilePosition - startPosition;
        int distance = Mathf.Abs((int)direction.x) + Mathf.Abs((int)direction.z);
        return distance <= maxSteps && (diagonal || (Mathf.Abs(direction.x) == 1 && Mathf.Abs(direction.z) == 1));
    }
    public List<Vector3> GetWalkablePositions(Vector3 startPosition, int maxSteps, bool diagonal)
    {
        List<Vector3> walkablePositions = new List<Vector3>();

        // Calculate walkable positions
        for (int x = -maxSteps; x <= maxSteps; x++)
        {
            for (int y = -maxSteps; y <= maxSteps; y++)
            {
                if (x == 0 && y == 0) continue;

                if (Mathf.Abs(x) + Mathf.Abs(y) <= maxSteps)
                {
                    if (diagonal || (Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1))
                    {
                        Vector3 newPosition = startPosition + new Vector3(x, 0, y);
                        if (walkableTiles.Contains(newPosition))
                        {
                            walkablePositions.Add(newPosition);
                        }
                    }
                }
            }
        }

        return walkablePositions;
    }
    void OnTileClicked(Vector3 position)
    {
        character.SetTargetPosition(position);
    }
}
