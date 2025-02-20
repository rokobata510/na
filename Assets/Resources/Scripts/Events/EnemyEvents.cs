
using UnityEngine.Events;

public class EnemyEvents : ActorEvents
{
    public UnityEvent<IDealsDamage> onKnockedBack = new();
}