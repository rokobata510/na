
using System.Collections.Generic;
using UnityEngine;

public class TriggerItemEffectOnWallContactProjectile : AProjectile
{
    public APointTargetingEffect effect;
    protected override void WallInteraction()
    {
        effect.TriggerEffect(new List<GameObject> { gameObject });
        Destroy(gameObject);
    }
}

