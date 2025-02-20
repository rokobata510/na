using UnityEngine;

[CreateAssetMenu(menuName = "Items/Effects/ProjectileAttackerTargeting/SetAutoFireEffect")]
public class SetAutoFireEffect: AProjectileAttackerTargetingEffect
{
    public bool autoFire;

    public override void TriggerProjectileAttackerTargetingEffect(AProjectileWeaponAttacker target)
    {
        target.autoFire = autoFire;
    }
}
