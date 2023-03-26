using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class PixelArtDrawSystem : MonoBehaviour
{
    public static PixelArtDrawSystem Instance { get; private set; }

    public EventHandler OnColorChanged;

    public enum CursorSize
    {
        Small,
        Medium,
        Large
    }

    [SerializeField] private GameObject TestMesh;
    [SerializeField] private Texture2D colorTexture2D;
    private GridClass<GridObject> grid;

    private float cellSize = 1f;
    private Vector2 colorUV;
    Vector3 mousePosition;
    private CursorSize cursorSize;

    void Awake()
    {
        Instance = this;

        grid = new GridClass<GridObject>(100, 100, cellSize, Vector3.zero, (GridClass<GridObject> g, int x, int y) => new GridObject(g, x, y));
        colorUV = new Vector2(0, 0);
        cursorSize = CursorSize.Small;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPosition = Utils.GetMouseWorldPosition();
            int cursorSize = GetCursorSizeInt();
            for (int x = 0; x < cursorSize; x++)
            {
                for (int y = 0; y < cursorSize; y++)
                {
                    Vector3 gridWorldPosition = mouseWorldPosition + new Vector3(x, y) * cellSize;
                    GridObject pixelGridObject = grid.GetGridObject(gridWorldPosition);
                    if (pixelGridObject != null)
                    {
                        pixelGridObject.SetColorPosition(colorUV);
                    }
                }
            }

            // Color picker
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                colorUV = raycastHit.textureCoord;
                OnColorChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public GridClass<GridObject> GetGrid()
    {
        return grid;
    }

    public Vector2 GetColorUV()
    {
        return colorUV;
    }

    public void SetCursorSize(CursorSize cursorSize)
    {
        this.cursorSize = cursorSize;
    }

    private int GetCursorSizeInt()
    {
        switch(cursorSize)
        {
            default:
            case CursorSize.Small: return 1;
            case CursorSize.Medium: return 3;
            case CursorSize.Large: return 7;
        }
    }

    public void ExportImage()
    {
        Texture2D texture2D = new Texture2D(grid.GetWidth(), grid.GetHeigth(), TextureFormat.ARGB32, false);
        texture2D.filterMode = FilterMode.Point;

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeigth(); y++)
            {
                GridObject gridObject = grid.GetGridObject(x, y);

                Vector2 pixelCoordinates = gridObject.GetColorPosition();

                pixelCoordinates.x *= colorTexture2D.width;
                pixelCoordinates.y *= colorTexture2D.height;

                texture2D.SetPixel(x, y, colorTexture2D.GetPixel((int)pixelCoordinates.x, (int)pixelCoordinates.y));
            }
        }

        texture2D.Apply();

        byte[] byteArray = texture2D.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/pixelArt.png", byteArray);
    }

    public void SaveGridtoPC()
    {
        string filePath = Application.persistentDataPath + "/grid.dat";
        SaveGrid(GetGrid(), filePath); 
    }

    public void LoadGridFromPC()
    {
        string filePath = Application.persistentDataPath + "/grid.dat"; 
        GridClass<GridObject> loadedGrid = LoadGrid(filePath);
    }

    public void SaveGrid(GridClass<GridObject> gridObject, string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Create);
        formatter.Serialize(stream, gridObject);
        stream.Close();
    }


    public GridClass<GridObject> LoadGrid(string filePath)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open);
        GridClass<GridObject> gridObject = (GridClass<GridObject>)formatter.Deserialize(stream);
        stream.Close();
        return gridObject;
    }

    [Serializable]
    public class GridObject
    {
        private GridClass<GridObject> grid;
        private int x;
        private int y;
        private SerializableVector3 colorPosition;

        public GridObject(GridClass<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetColorPosition(Vector3 position)
        {
            this.colorPosition = new SerializableVector3(position);
            grid.TriggerGridObjectChanged(x, y);
        }

        public Vector3 GetColorPosition()
        {
            if (colorPosition != null)
            {
                return colorPosition.ToVector3();
            }
            else
            {
                return Vector3.zero;
            }
        }

        private void TriggerGridObjectChanged()
        {
            grid.TriggerGridObjectChanged(x, y);
        }

        public override string ToString()
        {
            return GetColorPosition().ToString();
        }
    }
}
