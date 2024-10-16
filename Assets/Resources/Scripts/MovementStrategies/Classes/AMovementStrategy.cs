using System;
using UnityEngine;

public abstract class AMovementStrategy : ScriptableObject
{
    public AOverridenVector3 nextStepPosition = new UnnormalizedVector3();

    public virtual AOverridenVector3 GetNextStep(UnnormalizedVector3 origin, UnnormalizedVector3 sizeOfUser)
    {
        SetMovementDirection(origin);
        Debug.Log("targetPosition: " + nextStepPosition);
        return nextStepPosition;
    }

    protected abstract void SetMovementDirection(UnnormalizedVector3 origin);

    public AMovementStrategy Clone()
    {
        return (AMovementStrategy)MemberwiseClone();
    }
}

