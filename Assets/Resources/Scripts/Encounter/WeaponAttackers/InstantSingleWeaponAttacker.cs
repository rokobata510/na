
using UnityEngine;

[CreateAssetMenu(fileName = "InstantSingleWeaponAttacker", menuName = "WeaponAttackers/InstantSingleWeaponAttacker")]

public class InstantSingleWeaponAttacker : ASingleWeaponAttacker
{
    public override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        NormalizedVector3 direction = (targetPosition - userGameObject.transform.position).normalized;
        if (NeedsNewKeyPressForNonAutoWeapons() || !CooledDown(currentTime))
        {
            return;
        }
        ProjectileSpawner.InstantiateProjectile(GetProps(userGameObject.transform.position + direction * projectileOffset, direction, layer));
        timeOfLastAttack = currentTime;
        attackKeyWasReleased = false;
    }
}

