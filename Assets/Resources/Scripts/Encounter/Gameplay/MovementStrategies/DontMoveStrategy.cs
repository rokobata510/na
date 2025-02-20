using UnityEngine;

[CreateAssetMenu(fileName = "DontMoveStrategy", menuName = "MovementStrategies/DontMoveStrategy")]
public class DontMoveStrategy : AEnemyMovementStrategy
{
    protected override void SetMovementDirection(GameObject origin)
    {
        return;
    }
}

