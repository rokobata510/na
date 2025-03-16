using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackStrategy", menuName = "AttackStrategies/PlayerAttackStrategy")]

public class PlayerAttackStrategy : AAttackStrategy
{
    InputActions inputActions;
    public void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.PlayerActions.Enable();
        Debug.Log("Player Attack strategy is now enabled");
    }
    public override bool WantsToAttack(UnnormalizedVector3 origin) => inputActions.PlayerActions.Attack.IsPressed() && !PauseMenuManager.isPaused;
}

