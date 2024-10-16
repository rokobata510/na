using UnityEditor.Experimental.GraphView;
using UnityEngine.Events;

public class AttackableEvents
{
    public UnityEvent<ADealsDamage> OnDamaged = new();
    public UnityEvent OnDeath = new();
    public UnityEvent OnKnockBack = new();
}
