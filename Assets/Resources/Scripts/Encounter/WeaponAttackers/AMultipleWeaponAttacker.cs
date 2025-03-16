using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AMultipleWeaponAttacker : AProjectileWeaponAttacker
{
    public List<ASingleWeaponAttacker> weaponAttackers;
    protected List<ASingleWeaponAttacker> weaponAttackersCopy;
    protected int attackerIndex;

    public override float FireRate { get => weaponAttackersCopy[attackerIndex].FireRate; set => weaponAttackersCopy[attackerIndex].FireRate = value; }
    public override int Damage { get => weaponAttackersCopy[attackerIndex].Damage; set => weaponAttackersCopy[attackerIndex].Damage = value; }
    public override AProjectile Projectile { get => weaponAttackersCopy[attackerIndex].Projectile; set => weaponAttackersCopy[attackerIndex].Projectile = value; }

    public override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        weaponAttackersCopy[attackerIndex].Attack(userGameObject, targetPosition, currentTime, layer);
        if (weaponAttackersCopy[attackerIndex].timeOfLastAttack == currentTime)
        {
            attackerIndex = PickNextAttackerIndex();
            weaponAttackersCopy.ForEach(weaponAttacker => weaponAttacker.timeOfLastAttack = currentTime);
            this.timeOfLastAttack = currentTime;
        }
        else
        {
            weaponAttackersCopy[attackerIndex].DontAttack(userGameObject);
        }
    }

    public virtual void OnEnable()
    {
        weaponAttackersCopy = weaponAttackers.Select(weaponAttacker => Instantiate(weaponAttacker)).ToList();
    }

    protected abstract int PickNextAttackerIndex();
}