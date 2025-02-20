using UnityEngine;

[CreateAssetMenu(fileName = "MoveUpStrategy", menuName = "MovementStrategies/MoveUpStrategy")]
public class MoveUpStrategy : AEnemyMovementStrategy
{
    protected override void SetMovementDirection(GameObject origin)
    {
        nextStepPosition = new NodeVector3(0, 1);
    }
    public override AOverridenVector3 GetNextStep(GameObject origin)
    {
        SetMovementDirection(origin);
        return nextStepPosition;
    }
}
