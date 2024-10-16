using UnityEngine;
[CreateAssetMenu(fileName = "DefaultEnemyAttackStrategy", menuName = "ScriptableObjects/AttackStrategies/DefaultEnemyAttackStrategy", order = 1)]


public class DefaultEnemyAttackStrategy : AEnemyAttackStrategy
{
    public override bool WantsToAttack(UnnormalizedVector3 origin) => SightChecker.CanSeeTarget(origin, targetDirection, attackRange);
}
