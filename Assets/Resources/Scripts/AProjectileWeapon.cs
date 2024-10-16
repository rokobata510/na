
using System;
using System.Collections;
using UnityEngine;

public abstract class AProjectileWeapon : AWeapon
{
    public float projectileSpeed;
    public float distanceFromUser;
    public float projectileLifetime;
    public float forwardsOffset;
    public float fireRate;
    public bool justAttacked;
    public bool autoFire;
    public bool attackKeyWasReleased;

    public float rotationOffset = 0.125f;
    public GameObject projectile;

    public IEnumerator Cooldown()
    {
        justAttacked = true;
        yield return new WaitForSeconds(1 / fireRate);
        justAttacked = false;
    }
    public override void Attack(NormalizedVector3 direction)
    {
        if (justAttacked || NeedsNewKeyPressForNonAutoWeapons())
        {
            return;
        }
        GameObject projectileGameObject = InstantiateProjectile(direction);
        AProjectile projectileScript = projectileGameObject.GetComponent<AProjectile>();
        projectileScript.Fire(direction);
        StartCoroutine(Cooldown());
        attackKeyWasReleased = false;

    }
    public override void DontAttack()
    {
        attackKeyWasReleased = true; 
    }
    protected GameObject InstantiateProjectile(Vector3 position, NormalizedVector3 direction)
    {
        GameObject projectileGameObject = Instantiate(projectile, position, Quaternion.identity);
        projectileGameObject.tag = gameObject.tag;
        AProjectile projectileScript = projectileGameObject.GetComponent<AProjectile>();
        projectileGameObject.tag = gameObject.tag;
        projectileGameObject.layer = gameObject.layer;
        projectileScript.Damage = damage;
        projectileScript.Speed = projectileSpeed;
        projectileScript.LifetimeInSeconds = projectileLifetime;
        projectileScript.Fire(direction);
        StartCoroutine(Cooldown());
        return projectileGameObject;
    }
    protected virtual GameObject InstantiateProjectile(NormalizedVector3 direction) => InstantiateProjectile(transform.position, direction);

    protected bool NeedsNewKeyPressForNonAutoWeapons() => !autoFire && !attackKeyWasReleased;
}


