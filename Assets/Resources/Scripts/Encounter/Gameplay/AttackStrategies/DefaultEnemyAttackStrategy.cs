using UnityEngine;
[CreateAssetMenu(fileName = "DefaultEnemyAttackStrategy", menuName = "AttackStrategies/DefaultEnemyAttackStrategy")]


public class DefaultEnemyAttackStrategy : AEnemyAttackStrategy
{
    public override bool WantsToAttack(UnnormalizedVector3 origin) => SightChecker.CanSeeTarget(origin, targetDirection, attackRange);
}
