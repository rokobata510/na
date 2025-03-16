using UnityEngine;
using UnityEngine.Events;

public class AttackableEvents
{
    public UnityEvent<GameObject, IDealsDamage> OnDamaged = new();
    public UnityEvent OnDeath = new();
    public UnityEvent OnKnockBack = new();
}
