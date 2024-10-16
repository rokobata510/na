using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpStrategy", menuName = "ScriptableObjects/AttackStrategies/AttackUpStrategy", order = 1)]
public class AttackUpStrategy : AAttackStrategy
{
    public override void SetTarget(UnnormalizedVector3 origin)
    {
        targetDirection = new NormalizedVector3(0, 100);
    }

    public override bool WantsToAttack(UnnormalizedVector3 origin)
    {
        return true;
    }

}