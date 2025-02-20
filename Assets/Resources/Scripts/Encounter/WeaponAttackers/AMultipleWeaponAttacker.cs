
using System.Collections.Generic;
using UnityEngine;

public abstract class AMultipleWeaponAttacker : AProjectileWeaponAttacker
{ 

    public List<ASingleWeaponAttacker> weaponAttackers;
    protected int attackerIndex ;

    public override float FireRate { get => weaponAttackers[attackerIndex].FireRate; set => weaponAttackers[attackerIndex].FireRate = value; }
    public override int Damage { get => weaponAttackers[attackerIndex].Damage; set => weaponAttackers[attackerIndex].Damage = value; }
    public override AProjectile Projectile { get => weaponAttackers[attackerIndex].Projectile; set => weaponAttackers[attackerIndex].Projectile = value; }
    public override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        weaponAttackers[attackerIndex].Attack(userGameObject, targetPosition, currentTime, layer);
        if (weaponAttackers[attackerIndex].timeOfLastAttack == currentTime)
        {
            attackerIndex = PickNextAttackerIndex();
            weaponAttackers.ForEach(weaponAttacker => weaponAttacker.timeOfLastAttack = currentTime);
            this.timeOfLastAttack = currentTime;
        }
        else
        {
            weaponAttackers[attackerIndex].DontAttack(userGameObject);
        }
    }
    public virtual void Awake()
    {
        
    }
    protected abstract int PickNextAttackerIndex();
}

