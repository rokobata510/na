using UnityEngine;


public abstract class AAttackStrategy: ScriptableObject
{

    public float attackRange;
    public NormalizedVector3 targetDirection;
    
    public abstract void SetTarget(UnnormalizedVector3 origin);
    public abstract bool WantsToAttack(UnnormalizedVector3 origin);
    public AAttackStrategy Clone()
    {
        return (AAttackStrategy)MemberwiseClone();
    }

}


