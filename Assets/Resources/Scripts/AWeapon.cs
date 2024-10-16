public abstract class AWeapon : MonoBehaviourWithYLevelHandler
{
    public int damage;
    public float knockback;
    public abstract void Attack(NormalizedVector3 direction);
    public virtual void DontAttack() { }

    public void Die()
    {
        Destroy(gameObject);
    }

    public abstract void Point(UnnormalizedVector3 positionOfOwner, NormalizedVector3 direction);

}

