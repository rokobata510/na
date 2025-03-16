using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MultiplyInvincibilityFramesEffect", menuName = "Items/Effects/AttackableTargeting/MultiplyInvincibilityFramesEffect")]
public class MultiplyInvincibilityFramesEffect : AEffect
{
    public float invincibilityFrameMultiplier;
    public override void TriggerEffect(List<GameObject> targets)
    {
        foreach (GameObject target in targets)
        {
            if (target.TryGetComponent(out AAttackable attackableTarget))
            {
                attackableTarget.invincibilityTime *= invincibilityFrameMultiplier;
            }
        }
    }
}

