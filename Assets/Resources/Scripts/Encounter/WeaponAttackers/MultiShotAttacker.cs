using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MultiShotAttacker", menuName = "WeaponAttackers/MultiShotAttacker")]
public class MultiShotAttacker : ASingleWeaponAttacker
{
    public float secondsBetweenShots;
    public int numberOfBullets;

    protected int MillisecondsBetweenShots => (int)(secondsBetweenShots * 1000);

    public async override void Attack(GameObject userGameObject, UnnormalizedVector3 targetPosition, float currentTime, int layer)
    {
        NormalizedVector3 direction = (targetPosition - userGameObject.transform.position).normalized;
        if (NeedsNewKeyPressForNonAutoWeapons() || !CooledDown(currentTime))
        {
            return;
        }
        timeOfLastAttack = currentTime;
        attackKeyWasReleased = false;
        for (int i = 0; i < numberOfBullets; i++)
        {
            direction = userGameObject.GetComponent<AActor>().attackStrategyInstance.targetDirection;
            ProjectileSpawner.InstantiateProjectile(GetProps(userGameObject.transform.position + direction * projectileOffset, direction, layer));

            if (i < numberOfBullets - 1)
            {
                await Task.Delay(MillisecondsBetweenShots);
            }
        }

    }
}