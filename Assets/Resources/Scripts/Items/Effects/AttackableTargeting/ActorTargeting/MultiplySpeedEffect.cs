using UnityEngine;
[CreateAssetMenu(fileName = "MultiplySpeedEffect", menuName = "Items/Effects/ActorTargeting/MultiplySpeedEffect")]
public class MultiplySpeedEffect : AActorTargetingEffect
{
    public float speedMultiplier;
    public override void TriggerAActorTargetingEffect(AActor target)
    {
        target.MovementSpeed *= speedMultiplier;
    }
}