
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MultipleEffect", menuName = "Items/Effects/MultipleEffect")]
public class MultipleEffect : AEffect
{
    public List<AEffect> effects;
    public override void TriggerEffect(List<GameObject> targets)
    {
        foreach (AEffect effect in effects)
        {
            effect.TriggerEffect(targets);
        }
    }
}

