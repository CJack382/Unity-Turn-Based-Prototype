using UnityEngine;
using System.Collections.Generic;
using System;

public class GridManager : MonoBehaviour
{
    public int width = 8;
    public int height = 4;

    public GameObject gridCellPrefab; //Assign in Inspector
    public List<GameObject> gridObjects = new List<GameObject>();
    public GameObject[,] gridCells; //2D arrays are back baby

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        Vector2 centerOffset = new Vector2(width / 2.0f - 0.5f, height / 2.0f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPosition = new Vector2(x, y);
                Vector2 spawnPosition = gridPosition - centerOffset;

                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
