using UnityEngine;

[CreateAssetMenu(fileName = "MultiplyDamageEffect", menuName = "Items/Effects/WeaponAttackerTargeting/MultiplyDamageEffect")]
public class MultiplyDamageEffect : AWeaponAttackerTargetingEffect
{
    public float damageMultiplier;

    public override void TriggerWeaponAttackerTargetingEffect(AWeaponAttacker target)
    {
        target.Damage *= Mathf.RoundToInt(target.Damage * damageMultiplier);
    }
}