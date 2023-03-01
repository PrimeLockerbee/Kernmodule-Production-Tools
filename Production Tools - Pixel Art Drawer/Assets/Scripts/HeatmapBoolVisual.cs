using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapBoolVisual : MonoBehaviour
{
    private GridClass<bool> grid;
    private Mesh mesh;

    private bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(GridClass<bool> grid)
    {
        this.grid = grid;
        UpdateHeatmapVisual();

        grid.OnGridValueChanged += GridOnGridValueChanged;
    }

    private void GridOnGridValueChanged(object sender, GridClass<bool>.OnGridValueChangedEventArgs e)
    {
        updateMesh = true;
    }

    private void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = true;
            UpdateHeatmapVisual();
        }
    }

    private void UpdateHeatmapVisual()
    {
        Utils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeigth(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeigth(); y++)
            {
                int index = x * grid.GetHeigth() + y;

                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                bool gridValue = grid.GetValue(x, y);

                float gridValueNormalized = gridValue ? 1f : 0f;

                Vector2 gridValueUv = new Vector2(gridValueNormalized, 0f);

                Utils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridValueUv, gridValueUv);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
