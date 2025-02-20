using UnityEngine;
[CreateAssetMenu(fileName = "MultiptyKnockbackEffect", menuName = "Items/Effects/WeaponAttackerTargeting/MultiptyKnockbackEffect")]
public class MultiptyKnockbackEffect : AWeaponAttackerTargetingEffect
{
    public float knockbackMultiplier;

    public override void TriggerWeaponAttackerTargetingEffect(AWeaponAttacker target)
    {
        target.knockback *= knockbackMultiplier;
    }
}
