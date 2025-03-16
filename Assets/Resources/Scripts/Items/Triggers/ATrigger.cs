using UnityEngine;
using UnityEngine.Events;

public abstract class ATrigger : ScriptableObject
{
    public abstract void RegisterTrigger(UnityAction<object> effect, ItemEffectDirector director, object subject);
}

