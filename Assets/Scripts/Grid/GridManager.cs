using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private Tile _tilePrefab;

    private Dictionary<Vector2, Tile> _tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        float tileWidth = _tilePrefab.transform.localScale.x;

        float gridWidth = (_width - tileWidth);  // Subtract 10 because the last tile is at _width - 10
        float gridHeight = (_height - tileWidth);

        float xOffset = gridWidth / 2f;
        float yOffset = gridHeight / 2f;

        for (int x = 0; x < _width; x += (int)tileWidth)
        {
            for (int y = 0; y < _height; y += (int)tileWidth)
            {
                float adjustedX = x - xOffset;
                float adjustedY = y - yOffset;

                var spawnedTile = Instantiate(_tilePrefab, new Vector3(adjustedX, adjustedY, 0), Quaternion.identity);
                spawnedTile.name = $"Tile {adjustedX} {adjustedY}";

                var isOffset = ((x / (int)tileWidth) % 2 == 0 && (y / (int)tileWidth) % 2 != 0) || ((x / (int)tileWidth) % 2 != 0 && (y / (int)tileWidth) % 2 == 0);
                spawnedTile.Init(isOffset);

                _tiles[new Vector2(adjustedX, adjustedY)] = spawnedTile;
            }
        }
    }


    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}