using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Resources.Scripts.AttackStrategies.Classes
{
    [CreateAssetMenu(fileName = "NeverAttackStrategy", menuName = "AttackStrategies/NeverAttackStrategy")]

    internal class NeverAttackStrategy : AEnemyAttackStrategy
    {
        public override bool WantsToAttack(UnnormalizedVector3 origin) => false;
    }
}
