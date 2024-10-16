using UnityEngine;
[CreateAssetMenu(fileName = "PlayerMovementStrategy", menuName = "ScriptableObjects/MovementStrategies/PlayerMovementStrategy", order = 1)]
public class PlayerMovementStrategy : AMovementStrategy
{
    protected new UnnormalizedVector3 nextStepPosition;
    internal bool isRolling;

    protected override void SetMovementDirection(UnnormalizedVector3 origin)
    {
        if (isRolling)
            return;
        nextStepPosition = new UnnormalizedVector3(new((Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0),
                                                     (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0), 0));
    }
    public override AOverridenVector3 GetNextStep(UnnormalizedVector3 origin, UnnormalizedVector3 sizeOfUser)
    {
        SetMovementDirection(origin);
        return nextStepPosition;


    }


}

