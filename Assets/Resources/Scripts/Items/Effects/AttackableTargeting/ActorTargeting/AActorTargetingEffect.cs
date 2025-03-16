public abstract class AActorTargetingEffect : AAttackableTargetingEffect
{
    public sealed override void TriggerAttackableTargetingEffect(AAttackable target)
    {
        if (target is AActor actorTarget)
        {
            TriggerAActorTargetingEffect(actorTarget);
        }
    }
    public abstract void TriggerAActorTargetingEffect(AActor target);
}

