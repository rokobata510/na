public abstract class AWeaponAttackerTargetingEffect : AActorTargetingEffect
{
    public sealed override void TriggerAActorTargetingEffect(AActor target)
    {
        TriggerWeaponAttackerTargetingEffect(target.weaponScript.GetComponent<AWeapon>().WeaponAttacker);
    }
    public abstract void TriggerWeaponAttackerTargetingEffect(AWeaponAttacker target);
}

