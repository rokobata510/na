public abstract class AProjectileAttackerTargetingEffect : AWeaponAttackerTargetingEffect
{
    public sealed override void TriggerWeaponAttackerTargetingEffect(AWeaponAttacker target)
    {
        if (target is AProjectileWeaponAttacker attacker)
        {
            TriggerProjectileAttackerTargetingEffect(attacker);
        }
    }

    public abstract void TriggerProjectileAttackerTargetingEffect(AProjectileWeaponAttacker target);
}

