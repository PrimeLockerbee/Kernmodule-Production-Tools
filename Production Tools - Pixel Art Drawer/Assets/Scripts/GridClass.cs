using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridClass<GridObject>
{
    [field: NonSerialized]
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;

    [Serializable]
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;

    private float cellSize;

    private Vector3 originPosition;

    public GridObject[][] gridArray;

    public GridClass()
    {
    }

    public GridClass(int width, int height, float cellSize, Vector3 originPosition, Func<GridClass<GridObject>, int, int, GridObject> createGridObject) : this()
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new GridObject[width][];
        for (int x = 0; x < width; x++)
        {
            gridArray[x] = new GridObject[height];
            for (int y = 0; y < height; y++)
            {
                gridArray[x][y] = createGridObject(this, x, y);
            }
        }
    }

    public SerializableVector3 GetWorldPosition(int x, int y)
    {
        return new SerializableVector3(new Vector3(x, y) * cellSize + originPosition);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeigth()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    public void GetXY(SerializableVector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.ToVector3() - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition.ToVector3() - originPosition).y / cellSize);
    }

    public void SetGridObject(int x, int y, GridObject val)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            if (OnGridObjectChanged != null)
            {
                gridArray[x][y] = val;
                OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
            }
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null)
        {
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    public void SetGridObject(SerializableVector3 worldPosition, GridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public GridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x][y];
        }
        else
        {
            return default(GridObject);
        }
    }

    public GridObject GetGridObject(SerializableVector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}