using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponRenderer", menuName = "WeaponRenderers/MeleeWeaponRenderer")]
public class MeleeWeaponRenderer : AWeaponRenderer
{
    public bool weaponOnTheLeft;

    public override UnnormalizedVector3 CalculateGoalPosition(UnnormalizedVector3 positionOfOwner, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        float angle = Mathf.Atan2(pointingDirection.Y, pointingDirection.X) * Mathf.Rad2Deg;

        Vector3 offsetDirection = weaponOnTheLeft ? Vector3.down : Vector3.up;

        UnnormalizedVector3 newPosition = positionOfOwner + (UnnormalizedVector3)(Quaternion.Euler(0, 0, angle) * offsetDirection * distanceFromUser);

        return newPosition;
    }

    public override Quaternion CalculateGoalRotation(NormalizedVector3 pointingDirection)
    {
        float angle = Mathf.Atan2(pointingDirection.Y, pointingDirection.X) * Mathf.Rad2Deg;
        Quaternion newRotation = weaponOnTheLeft
            ? Quaternion.Euler(180, 0, -angle + rotationOffset * 360)
            : Quaternion.Euler(0, 0, angle + rotationOffset * 360);
        return newRotation;
    }
}