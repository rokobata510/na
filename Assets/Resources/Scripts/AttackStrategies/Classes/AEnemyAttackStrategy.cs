using UnityEngine;

public abstract class AEnemyAttackStrategy : AAttackStrategy
{
    public GameObject targetGameObject;
    public override void SetTarget(UnnormalizedVector3 origin)
    {
        GameObject player = GameObject.Find("Player");
        targetDirection = (player.transform.position-origin);
    }

}

