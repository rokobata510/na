using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiAirstrikeAttacker", menuName = "WeaponAttackers/MultiAirstrikeAttacker")]
public class MultiAirstrikeAttacker : ASingleWeaponAttacker
{
    public float lineLength;
    protected float HalfOfLineLength => lineLength / 2;
    public int countOfAttacks;
    public float secondsBetweenExplosions;
    protected int MilliSecondsBetweenExplosions => (int)(secondsBetweenExplosions * 1000);
    public AProjectile chargingProjectile;

    public async override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        if (NeedsNewKeyPressForNonAutoWeapons() || !CooledDown(currentTime))
        {
            return;
        }
        NormalizedVector3 direction = new(RandomManager.EncounterRange(-1f, 1f), RandomManager.EncounterRange(-1f, 1f));
        UnnormalizedVector3 startingPoint = targetPosition - direction * HalfOfLineLength;
        UnnormalizedVector3 endPoint = targetPosition + direction * HalfOfLineLength;
        UnnormalizedVector3 stepPerAttackFired = (endPoint - startingPoint) / countOfAttacks;
        timeOfLastAttack = currentTime;
        attackKeyWasReleased = false;
        GameObject[] chargingProjectileInstances = new GameObject[countOfAttacks];

        for (int numberOfCurrentAttack = 0; numberOfCurrentAttack < countOfAttacks; numberOfCurrentAttack++)
        {
            UnnormalizedVector3 targetOfCurrentAttack = startingPoint + stepPerAttackFired * numberOfCurrentAttack;
            chargingProjectileInstances[numberOfCurrentAttack] = ProjectileSpawner.InstantiateProjectile(GetAirstrikeChargingProps(targetOfCurrentAttack));
        }
        for (int numberOfCurrentAttack = 0; numberOfCurrentAttack < countOfAttacks; numberOfCurrentAttack++)
        {
            ExplodeChargingProjectile(chargingProjectileInstances[numberOfCurrentAttack], layer);
            await Task.Delay(MilliSecondsBetweenExplosions);
            if (!Application.isPlaying)
            {
                break;
            }
        }
    }

    private void ExplodeChargingProjectile(GameObject selectedChargingProjectile, int layer)
    {
        UnnormalizedVector3 targetOfCurrentAttack = selectedChargingProjectile.transform.position;
        Destroy(selectedChargingProjectile);
        if (!Application.isPlaying)
        {
            return;
        }
        ProjectileSpawner.InstantiateProjectile(GetAirstrikeProps(targetOfCurrentAttack, layer));
    }

    protected ProjectileSpawningProps GetAirstrikeProps(UnnormalizedVector3 targetPosition, int layer)
    {
        return new(projectile, targetPosition, new(0, 1, 0), damage, knockback, 0, projectileLifetime, layer);
    }

    protected ProjectileSpawningProps GetAirstrikeChargingProps(UnnormalizedVector3 position)
    {
        return new(chargingProjectile, position, new(0, 1, 0), 0, 0, 0, 3600, LayerMask.NameToLayer("NoCollision"));
    }
}

