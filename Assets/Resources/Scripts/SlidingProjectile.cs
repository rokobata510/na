using System.Collections;
using UnityEngine;
public class SlidingProjectile : AProjectile
{
    protected override void WallInteraction(NormalizedVector3 collisionNormal)
    {
        return;
    }
}

