using UnityEngine;
[CreateAssetMenu(menuName = "Items/Effects/ProjectileAttackerTargeting/MultiplyFirerateEffect")]
public class MultiplyFirerateEffect : AProjectileAttackerTargetingEffect
{
    public float firerateMultiplier;
    public override void TriggerProjectileAttackerTargetingEffect(AProjectileWeaponAttacker target)
    {
        target.FireRate *= firerateMultiplier;
    }
}

