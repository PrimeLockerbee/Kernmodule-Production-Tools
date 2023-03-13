using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelArtDrawSystem : MonoBehaviour
{
    [SerializeField] private PixelArtDrawingVisual pixelArtDrawingSystemVisual;
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
            colorUV = new Vector2(0, .3f);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            colorUV = new Vector2(0, 0);
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
