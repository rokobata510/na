using UnityEngine;
[CreateAssetMenu(fileName = "AddHealthEffect", menuName = "Items/Effects/AttackableTargeting/AddHealthEffect")]
public class AddHealthEffect : AAttackableTargetingEffect
{
    public int healthAmount;

    public override void TriggerAttackableTargetingEffect(AAttackable target)
    {
        target.health += healthAmount;
    }
}

