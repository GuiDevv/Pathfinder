using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    Dictionary<Vector2Int, int> distanceChart = new Dictionary<Vector2Int, int>();
    Dictionary<Vector2Int, Vector2Int> pathChart = new Dictionary<Vector2Int, Vector2Int>();

    public void PathFindingSearch(Vector2Int pos)
    {
        Vector2Int currentPos = pos;
        Queue<Vector2Int> frontier = new Queue<Vector2Int>();

        distanceChart.Clear();
        pathChart.Clear();


    }

}
