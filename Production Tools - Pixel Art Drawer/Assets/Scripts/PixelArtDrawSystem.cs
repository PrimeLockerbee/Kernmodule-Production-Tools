using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelArtDrawSystem : MonoBehaviour
{
    [SerializeField] private PixelArtDrawingVisual pixelArtDrawingSystemVisual;
    [SerializeField] private Texture2D colorTexture2D;
    private GridClass<GridObject> grid;

    private Vector2 colorUV;

    void Awake()
    {
        grid = new GridClass<GridObject>(10, 10, 1f, Vector3.zero, (GridClass<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    private void Start()
    {
        pixelArtDrawingSystemVisual.SetGrid(grid);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            grid.GetGridObject(mousePosition).SetColorUV(colorUV);
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            colorUV = new Vector2(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            colorUV = new Vector2(.3f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            colorUV = new Vector2(0, 0);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            SaveImage();
        }
    }

    private void SaveImage()
    {
        Texture2D texture2D = new Texture2D(grid.GetWidth(), grid.GetHeigth(), TextureFormat.ARGB32, false);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeigth(); y++)
            {
                GridObject gridObject = grid.GetGridObject(x, y);
                Vector2 gridUV00, gridUV11;

                gridUV00 = gridObject.GetColorUV();
                gridUV11 = gridObject.GetColorUV();

                int pixelX = (int)(gridObject.GetColorUV().x * colorTexture2D.width);
                int pixelY = (int)(gridObject.GetColorUV().y * colorTexture2D.height);

                colorTexture2D.GetPixel(pixelX, pixelY);
            }
        }
    }

    public class GridObject
    {
        private GridClass<GridObject> grid;
        private int x;
        private int y;
        private Vector2 colorUV;

        public GridObject(GridClass<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetColorUV(Vector2 colorUV)
        {
            this.colorUV = colorUV;
            grid.TriggerGridObjectChanged(x, y);
        }

        public Vector2 GetColorUV()
        {
            return colorUV;
        }

        public override string ToString()
        {
            return ((int)colorUV.x).ToString();
        }
    }
}
