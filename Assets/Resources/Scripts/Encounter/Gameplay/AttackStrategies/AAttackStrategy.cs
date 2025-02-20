using UnityEngine;


public abstract class AAttackStrategy: ScriptableObject
{
    public string targetName;
    public float attackRange;
    public NormalizedVector3 targetDirection;
    public GameObject targetGameObject;
    
    public abstract bool WantsToAttack(UnnormalizedVector3 origin);
    public AAttackStrategy Clone()
    {
        return (AAttackStrategy)MemberwiseClone();
    }
    public virtual void SetTarget(UnnormalizedVector3 origin)
    {
        if(targetGameObject == null)
        {
            targetGameObject = GameObject.Find(targetName);
        }

        targetDirection = targetGameObject.transform.position - origin;
    }

}


