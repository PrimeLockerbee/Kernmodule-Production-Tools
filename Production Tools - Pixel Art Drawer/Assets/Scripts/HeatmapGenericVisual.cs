using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapGenericVisual : MonoBehaviour
{
    private GridClass<HeatmapGridObject> grid;
    private Mesh mesh;

    private bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(GridClass<HeatmapGridObject> grid)
    {
        this.grid = grid;
        UpdateHeatmapVisual();

        grid.OnGridObjectChanged += GridOnGridValueChanged;
    }

    private void GridOnGridValueChanged(object sender, GridClass<HeatmapGridObject>.OnGridObjectChangedEventArgs e)
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

                HeatmapGridObject gridObject = grid.GetGridObject(x, y);

                float gridValueNormalized = gridObject.GetValueNormalized();

                Vector2 gridValueUv = new Vector2(gridValueNormalized, 0f);

                Utils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridValueUv, gridValueUv);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
