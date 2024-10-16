using UnityEngine.Events;

public class ActorEvents: AttackableEvents
{
    public UnityEvent OnWalking = new();
    public UnityEvent OnIdle = new();
    public UnityEvent OnDealsDamage = new();
    public UnityEvent OnKill = new();
}

