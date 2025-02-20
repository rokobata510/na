
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Effects/ProjectileAttackerTargeting/MultiplyProjectileSpeedEffect")]
public class MultiplyProjectileSpeedEffect : AProjectileAttackerTargetingEffect
{
    public float projectileSpeedMultiplier;
    public override void TriggerProjectileAttackerTargetingEffect(AProjectileWeaponAttacker target)
    {
        target.projectileSpeed *= projectileSpeedMultiplier;
    }
}

