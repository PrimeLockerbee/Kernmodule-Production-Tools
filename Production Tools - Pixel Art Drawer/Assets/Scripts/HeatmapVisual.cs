using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapVisual : MonoBehaviour
{
    //private GridClass grid;
    //private Mesh mesh;

    //private bool updateMesh;

    //private void Awake()
    //{
    //    mesh = new Mesh();
    //    GetComponent<MeshFilter>().mesh = mesh;
    //}

    //public void SetGrid(GridClass grid)
    //{
    //    this.grid = grid;
    //    UpdateHeatmapVisual();

    //    grid.OnGridValueChanged += GridOnGridValueChanged;
    //}

    //private void GridOnGridValueChanged(object sender, GridClass.OnGridValueChangedEventArgs e)
    //{
    //    Debug.Log("GridOnGridValueChanged");
    //    updateMesh = true;
    //}

    //private void LateUpdate()
    //{
    //    if (updateMesh)
    //    {
    //        updateMesh = true;
    //        UpdateHeatmapVisual();
    //    }
    //}

    //private void UpdateHeatmapVisual()
    //{
    //    Utils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeigth(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

    //    for (int x = 0; x < grid.GetWidth(); x++)
    //    {
    //        for (int y = 0; y < grid.GetHeigth(); y++)
    //        {
    //            int index = x * grid.GetHeigth() + y;

    //            Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

    //            int gridValue = grid.GetValue(x, y);

    //            float gridValueNormalized = (float)gridValue / GridClass.heatMapMaxVal;

    //            Vector2 gridValueUv = new Vector2(gridValueNormalized, 0f);

    //            Utils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridValueUv, gridValueUv);
    //        }
    //    }

    //    mesh.vertices = vertices;
    //    mesh.uv = uv;
    //    mesh.triangles = triangles;
    //}
}
