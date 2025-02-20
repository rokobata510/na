using log4net.Util;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class AProjectileWeaponAttacker : AWeaponAttacker
{
    public float projectileSpeed;
    public float projectileLifetime;
    public float projectileOffset;

    public bool attackKeyWasReleased;
    public bool autoFire;
    protected ProjectileSpawner projectileSpawner;
    public abstract float FireRate { get; set; }
    public abstract AProjectile Projectile { get; set; }
    protected ProjectileSpawner ProjectileSpawner { get => projectileSpawner != null ? projectileSpawner : CreateInstance<ProjectileSpawner>(); }

    public override void DontAttack(GameObject userGameObject)
    {
        attackKeyWasReleased = true;
    }
    protected ProjectileSpawningProps GetProps(UnnormalizedVector3 position, NormalizedVector3 direction, string tag, int layer)
    {
        return new(Projectile, position, direction, Damage, knockback, projectileSpeed, projectileLifetime, tag, layer);
    }
    protected ProjectileSpawningProps GetProps(UnnormalizedVector3 position, NormalizedVector3 direction, int layer)
    {
        return GetProps(position, direction, "Projectile", layer);

    }
    protected bool NeedsNewKeyPressForNonAutoWeapons() => !autoFire && !attackKeyWasReleased;

}