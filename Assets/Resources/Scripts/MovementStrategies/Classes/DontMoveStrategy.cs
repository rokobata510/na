using UnityEngine;

[CreateAssetMenu(fileName = "DontMoveStrategy", menuName = "ScriptableObjects/MovementStrategies/DontMoveStrategy", order = 1)]
public class DontMoveStrategy : AEnemyMovementStrategy
{
    protected override void SetMovementDirection(UnnormalizedVector3 origin)
    {
        return;
    }
}

