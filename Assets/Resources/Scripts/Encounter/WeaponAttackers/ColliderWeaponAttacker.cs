using UnityEngine;

[CreateAssetMenu(fileName = "ColliderAttacker", menuName = "WeaponAttackers/ColliderAttacker")]
public class ColliderWeaponAttacker : AWeaponAttacker, IDealsDamage
{
    public int damage;
    public override int Damage { get => damage; set => damage = value; }

    public float Knockback => knockback;

    public bool GivesInvincibilityFrames => true;

    public override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        int layerMask = 1 << layer;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(userGameObject.transform.position, 1.0f, ~layerMask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out AAttackable attackeable))
            {
                attackeable.GetAttacked(userGameObject, this);
            }
        }
    }

    public void TryToDealDamage(GameObject DealDamageToThisTarget)
    {
        if (DealDamageToThisTarget.TryGetComponent(out AAttackable attackable))
        {
            attackable.GetAttacked(DealDamageToThisTarget, this);
        }
    }
}

