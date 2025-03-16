using UnityEngine;
[CreateAssetMenu(fileName = "SpawnWhereverAttacker", menuName = "WeaponAttackers/SpawnWhereverAttacker")]

public class AirStrikeAttacker : ASingleWeaponAttacker
{

    public override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        NormalizedVector3 direction = (targetPosition - userGameObject.transform.position).normalized;
        if (NeedsNewKeyPressForNonAutoWeapons() || !CooledDown(currentTime))
        {
            return;
        }

    }



}
