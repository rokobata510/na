using UnityEngine;
[CreateAssetMenu(fileName = "OnUpdateItem", menuName = "Items/OnUpdateItem")]
public class OnUpdateItem : AItem
{
    public float cooldown;
    protected float timeSinceLastTrigger = 0;
    public override void Effect()
    {
        timeSinceLastTrigger += Time.deltaTime;
        if (timeSinceLastTrigger >= cooldown)
        {
            base.Effect();
            timeSinceLastTrigger = 0;
        }

    }
}

