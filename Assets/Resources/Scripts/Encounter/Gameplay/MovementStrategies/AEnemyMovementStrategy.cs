using System.Collections;
using UnityEngine;


public abstract class AEnemyMovementStrategy : AMovementStrategy
{
    protected GameObject CopyOfTargetGameObjectAtLastSeenPosition;
    protected UnnormalizedVector3 targetPosition = new();
    protected GameObject targetGameObject;
    protected UnnormalizedVector3 LastSeenTargetPosition => targetGameObject.transform.position;
    
}

