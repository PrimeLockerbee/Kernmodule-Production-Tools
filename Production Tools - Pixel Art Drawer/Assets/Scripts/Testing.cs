using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridClass grid;


    private void Start()
    {
        grid = new GridClass(4, 2, 10f, new Vector3(20,0,0));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(Utils.GetMouseWorldPosition(), 69);
        }
        
        if(Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(Utils.GetMouseWorldPosition()));
        }
    }

    private class HeatMapVisual
    {
        private GridClass grid;

        public HeatMapVisual(GridClass grid)
        {
            this.grid = grid;

            Vector3[] vertices;
            Vector2[] uvs;
            int[] triangles;

            Utils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeigth(), out vertices, out uvs, out triangles);
            for(int x = 0; x < grid.GetWidth(); x++)
            {
                for(int y = 0; y < grid.GetHeigth(); y++)
                {

                }
            }
        }
    }
}
