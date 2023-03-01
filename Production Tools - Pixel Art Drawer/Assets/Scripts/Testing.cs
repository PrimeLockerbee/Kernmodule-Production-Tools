using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private HeatmapVisual heatmapVisual;
    [SerializeField] private HeatmapBoolVisual heatmapBoolVisual;
    [SerializeField] private HeatmapGenericVisual heatmapGenericVisual;

    [SerializeField] private int gridWidth = 150;
    [SerializeField] private int gridHeigth = 100;
    [SerializeField] private int cellSize = 7;
    [SerializeField] private Vector3 gridOriginPosition = new Vector3(0, 0, 0);

    private GridClass<HeatmapGridObject> grid;
    private GridClass<StringGridObject> stringGrid;

    private void Start()
    {
        //grid = new GridClass<HeatmapGridObject>(gridWidth, gridHeigth, cellSize, gridOriginPosition, (GridClass<HeatmapGridObject> g, int x, int y) => new HeatmapGridObject(g, x, y));
        stringGrid = new GridClass<StringGridObject>(gridWidth, gridHeigth, cellSize, gridOriginPosition, (GridClass<StringGridObject> g, int x, int y) => new StringGridObject(g, x, y));

        //heatmapVisual.SetGrid(grid);
        //heatmapBoolVisual.SetGrid(grid);
        //heatmapGenericVisual.SetGrid(grid);
    }

    private void Update()
    {
        Vector3 position = Utils.GetMouseWorldPosition();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    HeatmapGridObject heatmapGridObject = grid.GetGridObject(position);

        //    if(heatmapGridObject != null)
        //    {
        //        heatmapGridObject.AddValue(5);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
            stringGrid.GetGridObject(position).AddLetter("A");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            stringGrid.GetGridObject(position).AddLetter("B");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            stringGrid.GetGridObject(position).AddLetter("C");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            stringGrid.GetGridObject(position).AddNumber("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stringGrid.GetGridObject(position).AddNumber("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            stringGrid.GetGridObject(position).AddNumber("3");
        }
    }
}

public class HeatmapGridObject
{
    private GridClass<HeatmapGridObject> grid;

    private const int Min = 0;
    private const int Max = 100;
    private int value;

    private int x;
    private int y;


    public HeatmapGridObject(GridClass<HeatmapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        Mathf.Clamp(value, Min, Max);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / Max;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}

public class StringGridObject
{
    private GridClass<StringGridObject> grid;

    private int x;
    private int y;


    private string letters;
    private string numbers;

    public StringGridObject(GridClass<StringGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        letters = "";
        numbers = "";
    }

    public void AddLetter(string letter)
    {
        letters += letter;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void AddNumber(string number)
    {
        numbers += number;
        grid.TriggerGridObjectChanged(x, y);
    }

    public override string ToString()
    {
        return letters + "\n" + numbers;
    }
}
