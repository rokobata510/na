using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackStrategy", menuName = "ScriptableObjects/AttackStrategies/PlayerAttackStrategy", order = 1)]

public class PlayerAttackStrategy : AAttackStrategy
{


    public override void SetTarget(UnnormalizedVector3 origin)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetDirection = mousePos - origin;
    }

    public override bool WantsToAttack(UnnormalizedVector3 origin)
    {
        return Input.GetMouseButton(0);
    }

}

