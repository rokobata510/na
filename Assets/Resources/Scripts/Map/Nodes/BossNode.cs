using UnityEngine;

public class BossNode : EnemyNode
{
    public override void EnterEncounter()
    {
        Debug.Log("Entered Boss Encounter at (" + column + " " + row);
    }
}

