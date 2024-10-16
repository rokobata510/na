
using UnityEngine;

public abstract class ADealsDamage : MonoBehaviourWithYLevelHandler
{
    [SerializeField] protected int damage;
    public int Damage { get => damage; set => damage = value; }
    public virtual float Knockback { get => damage;}
    public virtual void TryToDealDamage(GameObject DealDAmageToThisTarget)
    {
        if (DealDAmageToThisTarget.TryGetComponent(out ACanBeAttacked attackable))
        {
            attackable.Events.OnDamaged.Invoke(this);
            attackable.GetAttacked(this);
        }
    }
}

