using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelArtDrawSystem : MonoBehaviour
{
    private GridClass<GridObject> grid;

    void Awake()
    {
        grid = new GridClass<GridObject>(10, 10, 1f, Vector3.zero, (GridClass<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            grid.GetGridObject(mousePosition).SetColor(Color.red);
        }
    }

    private class GridObject
    {
        private GridClass<GridObject> grid;
        private int x;
        private int y;
        private Color color;

        public GridObject(GridClass<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetColor(Color color)
        {
            this.color = color;
            grid.TriggerGridObjectChanged(x, y);
        }

        public override string ToString()
        {
            return ((int)color.r).ToString();
        }
    }
}
