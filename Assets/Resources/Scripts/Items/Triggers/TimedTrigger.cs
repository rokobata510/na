
using UnityEngine.Events;

public class TimedTrigger : ATrigger
{
    public float SecondsBetweenProcs;
    public override void RegisterTrigger(UnityAction<object> effect, ItemEffectDirector director, object subject)
    {

    }
}

