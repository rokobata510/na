
using UnityEngine;

internal class ShopNode:AMapNode
    {
    public override void EnterEncounter()
    {
        Debug.Log("Entered Shop Encounter at (" + column + " " + row + ")");
    }
}

