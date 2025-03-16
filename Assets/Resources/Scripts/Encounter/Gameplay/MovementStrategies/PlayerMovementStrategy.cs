using UnityEngine.InputSystem;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

[CreateAssetMenu(fileName = "PlayerMovementStrategy", menuName = "MovementStrategies/PlayerMovementStrategy")]
public class PlayerMovementStrategy : AMovementStrategy
{
    internal bool isRolling;

    public InputActions inputActions;

    public void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.PlayerActions.Enable();
    }
    public void OnDisable()
    {
        inputActions.PlayerActions.Disable();
    }

    protected override void SetMovementDirection(GameObject origin)
    {
        if (!isRolling)
        {
            Vector2 movementInput = new Vector2(
                (inputActions.PlayerActions.MoveRight.IsPressed() ? 1 : 0) + 
                (inputActions.PlayerActions.MoveLeft.IsPressed() ? -1 : 0),
                (inputActions.PlayerActions.MoveUp.IsPressed() ? 1 : 0) + 
                (inputActions.PlayerActions.MoveDown.IsPressed() ? -1 : 0)
            ).normalized;
            nextStepPosition = new UnnormalizedVector3(movementInput);
        }
    }

    public override AOverridenVector3 GetNextStep(GameObject origin)
    {
        SetMovementDirection(origin);
        return nextStepPosition;
    }
}