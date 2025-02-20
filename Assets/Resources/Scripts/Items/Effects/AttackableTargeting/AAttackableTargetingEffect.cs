
using System.Collections.Generic;
using UnityEngine;
public abstract class AAttackableTargetingEffect : AEffect
{
    public sealed override void TriggerEffect(List<GameObject> targets)
    {
        foreach (GameObject target in targets)
        {
            if(target.TryGetComponent(out AAttackable AttackableTarget))
            {
                TriggerAttackableTargetingEffect(AttackableTarget);
            }
        }
    }
    public abstract void TriggerAttackableTargetingEffect(AAttackable target);
}

