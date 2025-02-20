using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpStrategy", menuName = "AttackStrategies/AttackUpStrategy")]
public class AttackUpStrategy : AAttackStrategy
{
    public override void SetTarget(UnnormalizedVector3 origin)
    {
        base.SetTarget(origin);
        targetDirection = new NormalizedVector3(0, 100);
    }

    public override bool WantsToAttack(UnnormalizedVector3 origin) => true;

}