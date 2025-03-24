
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MortarRenderer", menuName = "WeaponRenderers/MortarRenderer")]
public class MortarRenderer : AWeaponRenderer
{
    public override UnnormalizedVector3 CalculateGoalPosition(UnnormalizedVector3 ownerPosition, Transform weaponTransform, NormalizedVector3 pointingDirection)
    {
        float angle = Mathf.Atan2(0.5f, pointingDirection.X) * Mathf.Rad2Deg;
        UnnormalizedVector3 newPosition = new(
            ownerPosition +
            (Quaternion.Euler(0, 0, angle) * Vector3.right) +
            weaponTransform.rotation * new Vector3(1, 1, 0) * distanceFromUser);

        return newPosition;
    }

    public override Quaternion CalculateGoalRotation(NormalizedVector3 pointingDirection)
    {
        NormalizedVector3 absoluteValueVector = new(Math.Abs(pointingDirection.X), Math.Abs(pointingDirection.Y));
        float angle = Mathf.Atan2(pointingDirection.Y, pointingDirection.X) * Mathf.Rad2Deg;
        float cosineOfAngle = Mathf.Cos(angle * Mathf.Deg2Rad);
        NormalizedVector3 weaponPointingDirectionNormalized = new(cosineOfAngle, 1);
        Quaternion newRotation = Quaternion.Euler(0, 0, Mathf.Atan2(weaponPointingDirectionNormalized.Y, weaponPointingDirectionNormalized.X) * Mathf.Rad2Deg - rotationOffset * 360);
        return newRotation;
    }
}

