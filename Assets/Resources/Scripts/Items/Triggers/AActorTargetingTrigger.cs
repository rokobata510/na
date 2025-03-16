using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class AActorTargetingTrigger : ATrigger
{
    public sealed override void RegisterTrigger(UnityAction<object> effect, ItemEffectDirector director, object subject)
    {
        if (((GameObject)subject).TryGetComponent(out AActor actorSubject))
        {
            RegisterActorTargetingTrigger((subject) => effect(subject), director, actorSubject);
        }
        else
        {
            throw new ArgumentException("Subject must be of type AActor.", nameof(subject));
        }
    }
    public abstract void RegisterActorTargetingTrigger(UnityAction<AActor> effect, ItemEffectDirector director, AActor subject);
}

