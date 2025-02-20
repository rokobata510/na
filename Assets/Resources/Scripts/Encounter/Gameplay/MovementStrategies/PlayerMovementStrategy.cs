using UnityEngine;
[CreateAssetMenu(fileName = "PlayerMovementStrategy", menuName = "MovementStrategies/PlayerMovementStrategy")]
public class PlayerMovementStrategy : AMovementStrategy
{
    protected new UnnormalizedVector3 nextStepPosition;
    internal bool isRolling;

    protected override void SetMovementDirection(GameObject origin)
    {
        if (isRolling)
            return;
        nextStepPosition = new UnnormalizedVector3(new((Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0),
                                                     (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0), 0));
    }
    public override AOverridenVector3 GetNextStep(GameObject origin)
    {
        SetMovementDirection(origin);
        return nextStepPosition;


    }


}

