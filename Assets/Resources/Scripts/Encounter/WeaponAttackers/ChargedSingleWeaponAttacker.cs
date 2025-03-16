using UnityEngine;

[CreateAssetMenu(fileName = "ChargedSingleWeaponAttacker", menuName = "WeaponAttackers/ChargedSingleWeaponAttacker")]
public class ChargedSingleWeaponAttacker : ASingleWeaponAttacker
{
    public float maxChargeTime;
    public float minChargeTime;
    public float chargedFor = 0f;

    public AProjectile chargingProjectile;
    protected GameObject currentlyActiveChargingProjectile;
    protected NormalizedVector3 chargingDirection;

    public float SpeedMultiplierDuringWhileCharging;

    public override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        NormalizedVector3 direction = (targetPosition - userGameObject.transform.position).normalized;
        if (chargedFor == 0f)
        {
            ApplyChargeEffectsToUser(userGameObject);
            currentlyActiveChargingProjectile = ProjectileSpawner.InstantiateProjectile(GetChargeProps(userGameObject.transform.position + direction * projectileOffset, direction, layer));
        }
        if (chargedFor < maxChargeTime)
        {
            chargedFor += Time.deltaTime;
        }
        if (autoFire)
        {
            if (chargedFor >= maxChargeTime)
            {
                DontAttack(userGameObject);
            }
        }
        chargingDirection = direction;

        if (currentlyActiveChargingProjectile != null)
        {
            currentlyActiveChargingProjectile.transform.position = userGameObject.transform.position + direction * projectileOffset;


            float angle = Mathf.Atan2(direction.Y, direction.X) * Mathf.Rad2Deg - 90f;
            currentlyActiveChargingProjectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public override void DontAttack(GameObject userGameObject)
    {
        if (chargedFor != 0f)
        {
            RemoveChargeEffectsFromUser(userGameObject);
        }
        if (chargedFor >= minChargeTime)
        {
            ProjectileSpawningProps props = new(projectile, userGameObject.transform.position + chargingDirection * projectileOffset, chargingDirection, Damage, knockback, projectileSpeed, projectileLifetime, userGameObject.layer);
            ProjectileSpawner.InstantiateProjectile(props);
            timeOfLastAttack = Time.time;
            base.DontAttack(userGameObject);
        }
        if (currentlyActiveChargingProjectile != null)
            Destroy(currentlyActiveChargingProjectile);
        chargedFor = 0f;
    }

    protected ProjectileSpawningProps GetChargeProps(UnnormalizedVector3 position, NormalizedVector3 direction, int layer)
    {
        return new(chargingProjectile, position + direction * projectileOffset, direction, 0, 0, 0, 3600, LayerMask.NameToLayer("NoCollision"));
    }

    protected virtual void ApplyChargeEffectsToUser(GameObject userGameObject)
    {
        userGameObject.GetComponent<AActor>().MovementSpeed *= SpeedMultiplierDuringWhileCharging;
    }

    protected virtual void RemoveChargeEffectsFromUser(GameObject userGameObject)
    {
        userGameObject.GetComponent<AActor>().MovementSpeed /= SpeedMultiplierDuringWhileCharging;
    }
}