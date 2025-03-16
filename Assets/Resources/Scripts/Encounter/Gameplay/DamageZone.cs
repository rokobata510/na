using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class DamageZone : MonoBehaviourWithYLevelHandler, IDealsDamage
{
    [SerializeField] protected int damage;
    public int Damage { get => damage; set => damage = value; }

    public float Knockback => 0;

    public bool GivesInvincibilityFrames => true;

    public void OnTriggerStay2D(Collider2D collision)
    {
        TryToDealDamage(collision.gameObject);
    }

    public void TryToDealDamage(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out AAttackable attackable))
        {

            attackable.Events.OnDamaged.Invoke(gameObject, this);
            attackable.GetAttacked(gameObject, this);
        }
    }
}

