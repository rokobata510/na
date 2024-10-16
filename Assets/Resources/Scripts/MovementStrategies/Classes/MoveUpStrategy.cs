using UnityEngine;

[CreateAssetMenu(fileName = "MoveUpStrategy", menuName = "ScriptableObjects/MovementStrategies/MoveUpStrategy", order = 1)]
public class MoveUpStrategy : AEnemyMovementStrategy
{
    protected override void SetMovementDirection(UnnormalizedVector3 origin)
    {
        nextStepPosition = new NodeVector3(0, 1);
    }
    public override AOverridenVector3 GetNextStep(UnnormalizedVector3 origin, UnnormalizedVector3 sizeOfUser)
    {
        SetMovementDirection(origin);
        return nextStepPosition;
    }
}
