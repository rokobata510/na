using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughWallProjectile : AProjectile
{
    protected override void WallInteraction()
    {
        return;
    }
}
