using System.Collections;
using UnityEngine;


public abstract class AEnemyMovementStrategy : AMovementStrategy
{

    protected UnnormalizedVector3 lastSeenTargetPosition = new();
    protected UnnormalizedVector3 targetPosition = new();
    protected GameObject targetGameObject;
    public void OnEnable()
    {
        targetGameObject = GameObject.Find("Player");
    }
}

