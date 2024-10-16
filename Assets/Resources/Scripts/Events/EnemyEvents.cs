
using UnityEngine.Events;

public class EnemyEvents : ActorEvents
{
    public UnityEvent<ADealsDamage> onKnockedBack = new();
}