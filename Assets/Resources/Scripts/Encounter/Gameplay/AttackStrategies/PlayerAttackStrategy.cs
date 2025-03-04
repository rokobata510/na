using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackStrategy", menuName = "AttackStrategies/PlayerAttackStrategy")]

public class PlayerAttackStrategy : AAttackStrategy
{
    public override bool WantsToAttack(UnnormalizedVector3 origin) => Input.GetMouseButton(0) && !PauseMenuManager.isPaused;
}

