using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyTargeting", menuName = "Items/Targeting/EnemyTargeting")]
public class EnemyTargeting : ATargeting
{
    public override void FindAndSetAffectedGameObjects()
    {
        
        affectedGameObjects = GameObject.FindGameObjectsWithTag("Enemy").Where(x => x.TryGetComponent(out EnemyAI _)).ToList();
    }
}
