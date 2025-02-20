using UnityEngine;
[CreateAssetMenu(menuName = "Items/Effects/ProjectileAttackerTargeting/MulitplyProjectileLifetimeEffect")]
public class MulitplyProjectileLifetimeEffect : AProjectileAttackerTargetingEffect
{
    public float lifetimeMultiplier;

    public override void TriggerProjectileAttackerTargetingEffect(AProjectileWeaponAttacker target)
    {
        target.projectileLifetime *= lifetimeMultiplier;
    }
}

