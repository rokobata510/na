using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.XR;

public class MapNodeScriptableObject
{
    public GameObject Encounter;
    List<MapNode> routesFromHere;
    public List<MapNode> RoutesFromHere => routesFromHere;

    EncounterType type;
    public int row;
    public int column;
    public static int maxColumn = 5;
    public void GenerateChildren()
    {
        //for left ((this.column -1, but only if not less than 0), this.row-1), middle (this.column, this.row-1), and right (this.column_1, but only not not more than maxColumn, this.row-1) positions:
        //   if a 1 / 3 roll succeds
        //      call TryGenerate (generate a new node, or connect this to the node at the slot)),
        //if no connection exists, generate one on the middle
        //after finishing a row (reaching column 5)
        //  if row is above 1, decrement it by 1, and move onto the next row, from lest to right
        TryGenerate(column - 1, row - 1);
        TryGenerate(column, row - 1);
        TryGenerate(column + 1, row - 1);
    }

    public void TryGenerate(int row, int column)
    {
        
        //if a node exists at the slot, connect to it
        //else, generate a new node
    }

    public void ConnectThisToParamNode(MapNodeScriptableObject node)
    {
        //add node to routesFromHere
        routesFromHere.Add(node);
    }
}
