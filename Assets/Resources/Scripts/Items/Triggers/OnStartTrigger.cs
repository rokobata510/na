using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "OnStartTrigger", menuName = "Items/Triggers/OnStartTrigger")]
public class OnStartTrigger : ATrigger
{
    public override void RegisterTrigger(UnityAction<object> effect, ItemEffectDirector director, object subject)
    {
        effect.Invoke(subject);
    }
}

