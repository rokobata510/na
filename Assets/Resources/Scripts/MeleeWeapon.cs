using UnityEngine;
using UnityEngine.UIElements;
public class MeleeWeapon : AProjectileWeapon
{
    protected bool weaponOnTheLeft;

    public override void Attack(NormalizedVector3 direction)
    {
        if (justAttacked || NeedsNewKeyPressForNonAutoWeapons())
        {
            return;
        }
        weaponOnTheLeft = !weaponOnTheLeft;
        base.Attack(direction);
    }
    public override void Point(UnnormalizedVector3 positionOfOwner, NormalizedVector3 direction)
    {
        float angle = Mathf.Atan2(direction.Y, direction.X) * Mathf.Rad2Deg;
        UnnormalizedVector3 newPosition = positionOfOwner;
        Quaternion newRotation;
        if (weaponOnTheLeft)
        {
            newPosition += (UnnormalizedVector3)(Quaternion.Euler(0, 0, angle) * Vector3.down * distanceFromUser);
            newRotation = Quaternion.Euler(180, 0, -angle + rotationOffset * 360);
        }
        else
        {
            newPosition += (UnnormalizedVector3)(Quaternion.Euler(0, 0, angle) * Vector3.up * distanceFromUser);
            newRotation = Quaternion.Euler(0, 0, angle + rotationOffset * 360);
        }
        transform.SetPositionAndRotation(newPosition, newRotation);
    }

    protected override GameObject InstantiateProjectile(NormalizedVector3 direction)
    {
        float angle = Mathf.Atan2(direction.Y, direction.X) * Mathf.Rad2Deg;
        UnnormalizedVector3 instantiatePosition = new(transform.position);
        Vector3 offset = weaponOnTheLeft ? Vector3.down : Vector3.up;
        instantiatePosition += (UnnormalizedVector3)(Quaternion.Euler(0, 0, angle) * offset * distanceFromUser);
        instantiatePosition += (UnnormalizedVector3)(Quaternion.Euler(0, 0, angle) * Vector3.right * forwardsOffset);
        return InstantiateProjectile(instantiatePosition, Quaternion.Euler(0, 0, angle) * Vector3.right * distanceFromUser);
    }
}
