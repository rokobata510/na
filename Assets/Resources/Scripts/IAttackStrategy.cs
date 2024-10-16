using UnityEngine;

public interface IAttackStrategy<out T> where T : AProjectileWeapon
{
    void Attack(UnnormalizedVector3 position, NormalizedVector3 direction);
}