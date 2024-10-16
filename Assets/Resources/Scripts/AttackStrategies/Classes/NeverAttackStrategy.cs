using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Resources.Scripts.AttackStrategies.Classes
{
    [CreateAssetMenu(fileName = "NeverAttackStrategy", menuName = "ScriptableObjects/AttackStrategies/NeverAttackStrategy", order = 1)]

    internal class NeverAttackStrategy : AEnemyAttackStrategy
    {
        public override bool WantsToAttack(UnnormalizedVector3 origin)
        {
            return false;
        }
    }
}
