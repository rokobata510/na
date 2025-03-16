using UnityEngine;

public abstract class AWeaponAttacker : ScriptableObject
{

    public float knockback;
    public float timeOfLastAttack;

    public abstract int Damage { get; set; }

    public abstract void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer);

    public virtual void DontAttack(GameObject userGameObject)
    {
    }


}
