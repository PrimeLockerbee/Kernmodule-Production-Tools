using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridClass<bool> grid;

    [SerializeField] private HeatmapVisual heatmapVisual;
    [SerializeField] private HeatmapBoolVisual heatmapBoolVisual;

    [SerializeField] private int gridWidth = 150;
    [SerializeField] private int gridHeigth = 100;
    [SerializeField] private int cellSize = 7;
    [SerializeField] private Vector3 gridOriginPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        grid = new GridClass<bool>(gridWidth, gridHeigth, cellSize, gridOriginPosition);

        //heatmapVisual.SetGrid(grid);
        heatmapBoolVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = Utils.GetMouseWorldPosition();
            grid.SetValue(position, true);
        }
    }
}
