using UnityEngine;

namespace Assets.Resources.Scripts.AttackStrategies.Classes
{
    [CreateAssetMenu(fileName = "NeverAttackStrategy", menuName = "AttackStrategies/NeverAttackStrategy")]

    internal class NeverAttackStrategy : AEnemyAttackStrategy
    {
        public override bool WantsToAttack(UnnormalizedVector3 origin) => false;
    }
}
