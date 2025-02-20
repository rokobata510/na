using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MultiplyHealthEffect", menuName = "Items/Effects/AttackableTargeting/MultiplyHealthEffect")]
public class MultiplyHealthEffect : AAttackableTargetingEffect
{
    public float healthMultiplier;

    public override void TriggerAttackableTargetingEffect(AAttackable target)
    {
        target.health = Mathf.RoundToInt(target.health * healthMultiplier);
    }
}

