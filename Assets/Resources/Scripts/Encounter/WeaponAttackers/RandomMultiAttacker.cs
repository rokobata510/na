
using UnityEngine;

[CreateAssetMenu(menuName = "Encounter/WeaponAttackers/RandomMultiAttacker")]
public class RandomMultiAttacker : AMultipleWeaponAttacker
{
    public override float FireRate { get => weaponAttackers[attackerIndex].FireRate; set => weaponAttackers[attackerIndex].FireRate = value; }
    public override AProjectile Projectile { get => weaponAttackers[attackerIndex].Projectile; set => weaponAttackers[attackerIndex].Projectile = value; }


    protected override int PickNextAttackerIndex()
    {
        return Random.Range(0, weaponAttackers.Count);
    }
}

