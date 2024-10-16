using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public MapNodeScriptableObject scriptableObject;
    public void GenerateChildren()
    {
        SeededRandomStream.Range(1, 3);
        //for left ((this.column -1, but only if not less than 0), this.row-1), middle (this.column, this.row-1), and right (this.column_1, but only not not more than maxColumn, this.row-1) positions:
        //   if a 1 / 3 roll succeds
        //      call TryGenerate (generate a new node, or connect this to the node at the slot)),
        //if no connection exists, generate one on the middle
        //after finishing a row (reaching column 5)
        //  if row is above 1, decrement it by 1, and move onto the next row, from lest to right
        TryGenerate(scriptableObject.column - 1, scriptableObject.row - 1);
        TryGenerate(scriptableObject.column, scriptableObject.row - 1);
        TryGenerate(scriptableObject.column + 1, scriptableObject.row - 1);

    }
    public void TryGenerate(int row, int column)
    {
        //if a node exists at the slot, connect to it
        //else, generate a new node
    }
    public void ConnectThisToParamNode(MapNodeScriptableObject node)
    {
        //add node to routesFromHere
        scriptableObject.routesFromHere.Add(node);
    }
}
