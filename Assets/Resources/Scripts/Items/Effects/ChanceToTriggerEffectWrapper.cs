
using System.Collections.Generic;
using UnityEngine;

public class ChanceToTriggerEffectWrapper : AEffect
{
    public float triggerPercentage;
    public AEffect effect;
    public override void TriggerEffect(List<GameObject> targets)
    {
        int guaranteedProcs = (int)triggerPercentage / 100;
        float chanceForLastProc = triggerPercentage % 100;
        for (int i = 0; i< guaranteedProcs; i++)
            effect.TriggerEffect(targets);
        if (RandomManager.EncounterValue() * 100f <= chanceForLastProc)
        {
            effect.TriggerEffect(targets);
        }
    }
}

