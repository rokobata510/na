
using UnityEngine;

class EliteNode : EnemyNode
{
    public override void EnterEncounter()
    {
        Debug.Log("Entered Elite Encounter at (" + column + " " + row + ")");
    }
}

