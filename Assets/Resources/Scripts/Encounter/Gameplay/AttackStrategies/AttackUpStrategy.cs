using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpStrategy", menuName = "AttackStrategies/AttackUpStrategy")]
public class AttackUpStrategy : AAttackStrategy
{
    public override void SetTarget(UnnormalizedVector3 origin)
    {

        if (targetGameObject == null)
        {
            targetGameObject = new GameObject(targetName);
            targetDirection = new NormalizedVector3(0, 1);
        }

        targetGameObject.transform.position = origin + targetDirection;
    }

    public override bool WantsToAttack(UnnormalizedVector3 origin) => true;

}