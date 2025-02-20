
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponRenderer", menuName = "WeaponRenderers/RangedWeaponRenderer")]
public class RangedWeaponRenderer : AWeaponRenderer
{
    public override UnnormalizedVector3 CalculateGoalPosition(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        float angle = Mathf.Atan2(pointingDirection.Y, pointingDirection.X) * Mathf.Rad2Deg;
        UnnormalizedVector3 newPosition = new(
            ownerPosition +
            (Quaternion.Euler(0, 0, angle) * Vector3.right) +
            weaponTransform.rotation * new Vector3(1, 1, 0) * distanceFromUser);

        return newPosition;
    }

    public override Quaternion CalculateGoalRotation(NormalizedVector3 pointingDirection)
    {
        float angle = Mathf.Atan2(pointingDirection.Y, pointingDirection.X) * Mathf.Rad2Deg;
        Quaternion newRotation = Math.Abs(angle) > 90
            ? Quaternion.Euler(180, 0, -angle - rotationOffset * 360)
            : Quaternion.Euler(0, 0, angle - rotationOffset * 360);
        return newRotation;
    }
}

