using System;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnWhereverAttacker", menuName = "WeaponAttackers/SpawnWhereverAttacker")]

public class AirStrikeAttacker : ASingleWeaponAttacker
{

    public async override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        NormalizedVector3 direction = (targetPosition - userGameObject.transform.position).normalized;
        if (NeedsNewKeyPressForNonAutoWeapons() || !CooledDown(currentTime))
        {
            return;
        }
        
    }



}
