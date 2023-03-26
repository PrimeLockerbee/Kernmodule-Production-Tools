using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelArtDrawingVisual : MonoBehaviour
{
    [SerializeField] private PixelArtDrawSystem drawPixelArtSystem;
    private GridClass<PixelArtDrawSystem.GridObject> grid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Start()
    {
        SetGrid(drawPixelArtSystem.GetGrid());
    }

    public void SetGrid(GridClass<PixelArtDrawSystem.GridObject> grid)
    {
        this.grid = grid;

        UpdateVisual();

        grid.OnGridObjectChanged += Grid_OnGridValueChanged;
    }

    void Grid_OnGridValueChanged(object sender, GridClass<PixelArtDrawSystem.GridObject>.OnGridObjectChangedEventArgs e)
    {
        updateMesh = true;
    }

    private void LateUpdate()
    {
        if(updateMesh)
        {
            updateMesh = false;
            UpdateVisual();
        }
    }

    private void UpdateVisual()
    {
        Utils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeigth(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for(int x=0; x<grid.GetWidth(); x++)
        {
            for(int y=0;y<grid.GetHeigth(); y++)
            {
                int index = x * grid.GetHeigth() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();


                PixelArtDrawSystem.GridObject gridObject = grid.GetGridObject(x, y);
                Vector2 gridUV00, gridUV11;

                gridUV00 = new Vector2(gridObject.GetColorPosition().x, gridObject.GetColorPosition().y);
                gridUV11 = new Vector2(gridObject.GetColorPosition().x, gridObject.GetColorPosition().y);
                Vector3 worldPosition = grid.GetWorldPosition(x, y).ToVector3();
                Vector3 position = worldPosition + quadSize * .5f;
                Utils.AddToMeshArrays(vertices, uv, triangles, index, position, 0f, quadSize, gridUV00, gridUV11);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

}

public static class VectorExtensions
{
    public static Vector3 ToVector3(this SerializableVector3 vec)
    {
        return new Vector3(vec.x, vec.y, vec.z);
    }

    public static Vector2 ToVector2(this SerializableVector3 vec)
    {
        return new Vector2(vec.x, vec.y);
    }
}
